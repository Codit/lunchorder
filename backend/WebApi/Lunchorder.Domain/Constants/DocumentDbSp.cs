namespace Lunchorder.Domain.Constants
{
    public class DocumentDbSp
    {
        public const string AddUserOrder = "addUserOrder";
        public const string GetOrCreateVendorOrderHistory = "getOrCreateVendorOrderHistory";
        public const string UpdateUserBalance = "updateUserBalance";
        public const string AddToUserList = "addToUserList";
        public const string MarkAsSubmitted = "markAsSubmitted";
        public const string UpdateUserPicture = "updateUserPicture";
        public const string StorePushToken = "storePushToken";
        public const string DeletePushToken = "deletePushToken";
    }
}