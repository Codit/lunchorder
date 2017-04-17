using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Lunchorder.Common.Interfaces
{
    public interface IDocumentStore
    {
        IQueryable<T> GetItems<T>();
        IQueryable<T> GetItemsByExpression<T>(string sqlExpression);
        IQueryable<T> GetItems<T>(Expression<Func<T, bool>> predicate);
        Task<T> GetItem<T>(string sqlExpression);
        Task UpsertDocument<T>(T document);
        Task DeleteDocuments(string documentLink);
        Task CreateStoredProcedure(StoredProcedure storedProcedure, bool checkIfExists);
        Task CreateStoredProcedure(StoredProcedure storedProcedure, bool checkIfExists, bool overwrite);
        Task CreateDatabase();
        //Task CreateCollection(string collectionName, bool checkIfExists);
        //Task CreateCollection(bool checkIfExists);
        Task<T> ExecuteStoredProcedure<T>(string storedProcedureName, params dynamic[] parameters);
        Task UpsertDocument(object document);
        Task UpsertDocumentIfNotExists(string id, object document);
        Task<ResourceResponse<StoredProcedure>> GetStoredProcedure(string id);
        Task CreateIndex(IncludedPath index);
        Task SetIndexMode(IndexingMode mode);
        Document GetDocument(Expression<Func<Document, bool>> predicate);
        Task<ResourceResponse<Document>> ReplaceDocument(object document);
        Task CreateCollection();
    }
}
