using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json.Linq;

namespace Lunchorder.Dal
{
    public class DocumentStore : IDocumentStore
    {
        private readonly IConfigurationService _configurationService;

        public DocumentStore(IConfigurationService configurationService)
        {
            if (configurationService == null) throw new ArgumentNullException(nameof(configurationService));
            _configurationService = configurationService;

            DocumentDbEndpoint = _configurationService.DocumentDb.Endpoint;
            DocumentDbAuthKey = _configurationService.DocumentDb.AuthKey;
        }

        private static DocumentClient _documentDbClient;
        public static DocumentClient DocumentDbClient
        {
            get
            {
                
                return _documentDbClient ?? (_documentDbClient = new DocumentClient(new Uri(DocumentDbEndpoint), DocumentDbAuthKey, new ConnectionPolicy { EnableEndpointDiscovery = false,  ConnectionMode = ConnectionMode.Direct, ConnectionProtocol = Protocol.Https }));
            }
        }
        private Database _database;
        private DocumentCollection _documentCollection;

        private static string DocumentDbEndpoint { get; set; }
        private static string DocumentDbAuthKey { get; set; }

        
        private Uri CollectionUri
        {
            get
            {
                var uri = UriFactory.CreateDocumentCollectionUri(_configurationService.DocumentDb.Database,
                    _configurationService.DocumentDb.Collection);
                return uri;
            }
        }

        private Uri StoredProcedureUri(string procedure)
        {
                var uri = UriFactory.CreateStoredProcedureUri(_configurationService.DocumentDb.Database,
                    _configurationService.DocumentDb.Collection, procedure);
                return uri;
        }

        public IQueryable<T> GetItems<T>()
        {
            return this.GetItems<T>(null);
        }

        public IQueryable<T> GetItemsByExpression<T>(string sqlExpression)
        {
            return DocumentDbClient.CreateDocumentQuery<T>(CollectionUri, sqlExpression);
        }

        /// <summary>
        /// Creates the database if it does not exist using the database name from configuration setting
        /// </summary>
        /// <returns></returns>
        public async Task CreateDatabase()
        {
            var dbName = _configurationService.DocumentDb.Database;
            await DocumentDbClient.CreateDatabaseIfNotExistsAsync(new Database{Id = dbName});
        }

        public IQueryable<T> GetItems<T>(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                return DocumentDbClient.CreateDocumentQuery<T>(CollectionUri, new FeedOptions { MaxItemCount = -1 });

            return DocumentDbClient.CreateDocumentQuery<T>(CollectionUri, new FeedOptions { MaxItemCount = -1 })
                .Where(predicate);
        }

        public IQueryable<T> GetItemsOrderByDescending<T>(Expression<Func<T, bool>> wherePredicate, Expression<Func<T, DateTime>> orderPredicate)
        {
            if (wherePredicate == null)
                return DocumentDbClient.CreateDocumentQuery<T>(CollectionUri, new FeedOptions { MaxItemCount = -1 });

            return DocumentDbClient.CreateDocumentQuery<T>(CollectionUri, new FeedOptions { MaxItemCount = -1 })
                .Where(wherePredicate).OrderByDescending(orderPredicate);
        }

        public async Task CreateStoredProcedure(StoredProcedure storedProcedure, bool checkIfExists)
        {
            await CreateStoredProcedure(storedProcedure, checkIfExists, false);
        }

        public async Task<ResourceResponse<StoredProcedure>> GetStoredProcedure(string id)
        {
            StoredProcedure dbSp =
                    DocumentDbClient.CreateStoredProcedureQuery(CollectionUri)
                        .Where(c => c.Id == id)
                        .AsEnumerable()
                        .FirstOrDefault();
            return await DocumentDbClient.ReadStoredProcedureAsync(dbSp.SelfLink);
        }

        /// <summary>
        /// Creates a stored procedure on the document db database collection
        /// </summary>
        /// <param name="storedProcedure">The stored procedure to create</param>
        /// <param name="checkIfExists">Will only add the stored procedure if it does not exist</param>
        /// <returns></returns>
        public async Task CreateStoredProcedure(StoredProcedure storedProcedure, bool checkIfExists, bool overwrite)
        {
            if (checkIfExists || overwrite)
            {
                StoredProcedure dbSp =
                    DocumentDbClient.CreateStoredProcedureQuery(CollectionUri)
                        .Where(c => c.Id == storedProcedure.Id)
                        .AsEnumerable()
                        .FirstOrDefault();

                if (overwrite && dbSp != null)
                {
                    await DocumentDbClient.DeleteStoredProcedureAsync(dbSp.SelfLink);
                    dbSp = null;
                }

                if (dbSp == null)
                {
                    await CreateStoredProcedure(storedProcedure);
                }
            }
            else
            {
                await CreateStoredProcedure(storedProcedure);
            }
        }

        public async Task<T> ExecuteStoredProcedure<T>(string storedProcedureName, params dynamic[] parameters)
        {
            var storedProc = DocumentDbClient.CreateStoredProcedureQuery(CollectionUri).Where(p => p.Id == storedProcedureName).AsEnumerable().FirstOrDefault();

            if (storedProc == null)
            {
                throw new ArgumentNullException($"Stored Procedure {storedProcedureName} does not exists on the database");
            }

            StoredProcedureResponse<T> response = null;

            response = await DocumentDbClient.ExecuteStoredProcedureAsync<T>(storedProc.SelfLink, parameters);
            return response;
        }

        private async Task CreateStoredProcedure(StoredProcedure storedProcedure)
        {
            await DocumentDbClient.CreateStoredProcedureAsync(CollectionUri, storedProcedure);
        }

        public async Task UpsertDocument<T>(T document)
        {
            ResourceResponse<Document> result = await DocumentDbClient.UpsertDocumentAsync(
                CollectionUri,
                document);
        }

        public async Task UpsertDocument(object document)
        {
            ResourceResponse<Document> result = await DocumentDbClient.UpsertDocumentAsync(CollectionUri, JObject.FromObject(document));
        }

        public async Task UpsertDocumentIfNotExists(string id, object document)
        {
            var item = await GetItem<object>($"SELECT * FROM C WHERE C.id = '{id}'");
            if (item == null)
            {
                await UpsertDocument(document);
            }
        }

        public async Task DeleteDocuments(string documentLink)
        {
            ResourceResponse<Document> result = await DocumentDbClient.DeleteDocumentAsync(documentLink);
        }

        public async Task<T> GetItem<T>(string sqlExpression)
        {
            var documentQuery = DocumentDbClient.CreateDocumentQuery(CollectionUri, sqlExpression).AsDocumentQuery();
            var response =  await documentQuery.ExecuteNextAsync<T>();
            return response.FirstOrDefault();
        }

        public async Task<ResourceResponse<Document>> ReplaceDocument(object document)
        {
            ResourceResponse<Document> updated = await DocumentDbClient.UpsertDocumentAsync(CollectionUri, document);
            return updated;
        }

        public async Task CreateCollection()
        {
            await DocumentDbClient.CreateDocumentCollectionIfNotExistsAsync(
                UriFactory.CreateDatabaseUri(_configurationService.DocumentDb.Database),
                new DocumentCollection {Id = _configurationService.DocumentDb.Collection});
        }

        public Document GetDocument(Expression<Func<Document, bool>> predicate)
        {
            Document doc = DocumentDbClient.CreateDocumentQuery<Document>(CollectionUri)
                                        .Where(predicate)
                                        .AsEnumerable()
                                        .SingleOrDefault();

            return doc;
        }

        public async Task CreateIndex(IncludedPath index)
        {
            var collection = new DocumentCollection { Id = _configurationService.DocumentDb.Collection };

            collection.IndexingPolicy.IncludedPaths.Add(index);
            await DocumentDbClient.ReplaceDocumentCollectionAsync(collection);
        }

        public async Task SetIndexMode(IndexingMode mode)
        {
            var collection = new DocumentCollection { Id = _configurationService.DocumentDb.Collection };

            collection.IndexingPolicy.IndexingMode = mode;
            await DocumentDbClient.ReplaceDocumentCollectionAsync(collection);
        }
    }
}
