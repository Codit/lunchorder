namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// Used to specify the document db operation.
    /// </summary>
    public class ActionType
    {
        public const string Delete = "Delete";
        public const string AddOrUpdate = "AddOrUpdate";
    }
}