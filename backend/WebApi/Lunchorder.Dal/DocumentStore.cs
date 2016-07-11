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

        private DocumentClient _documentDbClient;
        private Database _database;
        private DocumentCollection _documentCollection;

        private string DocumentDbEndpoint { get; }
        private string DocumentDbAuthKey { get; }

        private DocumentClient DocumentDbClient
        {
            get
            {
                return _documentDbClient ?? (_documentDbClient = new DocumentClient(new Uri(DocumentDbEndpoint), DocumentDbAuthKey));
            }
        }

        private Database Database
            =>
                _database ??
                (_database = DocumentDbClient.CreateDatabaseQuery().Where(d => d.Id == _configurationService.DocumentDb.Database).AsEnumerable().FirstOrDefault());

        private DocumentCollection Collection
        {
            get
            {
                return _documentCollection ??
                       (_documentCollection = DocumentDbClient.CreateDocumentCollectionQuery(Database.SelfLink)
                           .Where(c => c.Id == _configurationService.DocumentDb.Collection)
                           .AsEnumerable()
                           .FirstOrDefault());
            }
        }


        public IQueryable<T> GetItems<T>()
        {
            return this.GetItems<T>(null);
        }

        public IQueryable<T> GetItemsByExpression<T>(string sqlExpression)
        {
            return DocumentDbClient.CreateDocumentQuery<T>(Collection.DocumentsLink, sqlExpression);
        }

        /// <summary>
        /// Creates the database if it does not exist using the database name from configuration setting
        /// </summary>
        /// <returns></returns>
        public async Task CreateDatabase()
        {
            var dbName = _configurationService.DocumentDb.Database;
            Database docDb = DocumentDbClient.CreateDatabaseQuery().Where(db => db.Id == dbName).ToArray().FirstOrDefault();
            if (docDb == null)
            {
                var database = new Database { Id = dbName };
                await DocumentDbClient.CreateDatabaseAsync(database);
            }
        }

        public IQueryable<T> GetItems<T>(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                return DocumentDbClient.CreateDocumentQuery<T>(Collection.DocumentsLink);

            return DocumentDbClient.CreateDocumentQuery<T>(Collection.DocumentsLink)
                .Where(predicate);
        }

        public async Task CreateStoredProcedure(StoredProcedure storedProcedure, bool checkIfExists)
        {
            await CreateStoredProcedure(storedProcedure, checkIfExists, false);
        }

        public async Task<ResourceResponse<StoredProcedure>> GetStoredProcedure(string id)
        {
            StoredProcedure dbSp =
                    DocumentDbClient.CreateStoredProcedureQuery(Collection.SelfLink)
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
                    DocumentDbClient.CreateStoredProcedureQuery(Collection.SelfLink)
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
            var storedProc = DocumentDbClient.CreateStoredProcedureQuery(Collection.StoredProceduresLink).Where(p => p.Id == storedProcedureName).AsEnumerable().FirstOrDefault();

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
            await DocumentDbClient.CreateStoredProcedureAsync(Collection.DocumentsLink, storedProcedure);
        }

        public async Task CreateCollection(bool checkIfExists)
        {
            await CreateCollection(_configurationService.DocumentDb.Collection, checkIfExists);
        }

        public async Task CreateCollection(string collectionName, bool checkIfExists)
        {
            var collectionDefinition = new DocumentCollection
            {
                Id = collectionName,
                IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 })
            };

            if (checkIfExists)
            {
                DocumentCollection dbCollection =
                    DocumentDbClient.CreateDocumentCollectionQuery(Database.SelfLink)
                        .Where(c => c.Id == collectionName)
                        .AsEnumerable()
                        .FirstOrDefault();

                if (dbCollection == null)
                {
                    await DocumentDbClient.CreateDocumentCollectionAsync(Database.SelfLink, collectionDefinition,
                            new RequestOptions());
                }
            }
            else
            {
                await DocumentDbClient.CreateDocumentCollectionAsync(Database.SelfLink, collectionDefinition, new RequestOptions());
            }
        }

        public async Task UpsertDocument<T>(T document)
        {
            ResourceResponse<Document> result = await DocumentDbClient.UpsertDocumentAsync(Collection.DocumentsLink, document);
        }

        public async Task UpsertDocument(string document)
        {
            ResourceResponse<Document> result = await DocumentDbClient.UpsertDocumentAsync(Collection.DocumentsLink, new JObject(document));
        }

        public async Task UpsertDocumentIfNotExists(string Id, string document)
        {
            var item = GetItem<object>($"SELECT * FROM C WHERE C.id = '{Id}'");
            if (item == null)
            {
                await UpsertDocument(document);
            }
        }

        public async Task DeleteDocuments(string documentLink)
        {
            ResourceResponse<Document> result = await DocumentDbClient.DeleteDocumentAsync(documentLink);
        }

        public IQueryable<dynamic> GetItem<T>(string sqlExpression)
        {
            return DocumentDbClient.CreateDocumentQuery(Collection.DocumentsLink, sqlExpression);

        }

        public async Task<Document> ReplaceDocument(Document document)
        {
            Document updated = await DocumentDbClient.ReplaceDocumentAsync(document);
            return updated;
        }

        public Document GetDocument(Expression<Func<Document, bool>> predicate)
        {
            Document doc = DocumentDbClient.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                                        .Where(predicate)
                                        .AsEnumerable()
                                        .SingleOrDefault();

            return doc;
        }

        public async Task CreateIndex(IncludedPath index)
        {
            Collection.IndexingPolicy.IncludedPaths.Add(index);
            await DocumentDbClient.ReplaceDocumentCollectionAsync(Collection);
        }

        public async Task SetIndexMode(IndexingMode mode)
        {
            Collection.IndexingPolicy.IndexingMode = mode;
            await DocumentDbClient.ReplaceDocumentCollectionAsync(Collection);
        }
    }
}
