function updateUserBalance(platformUserList) {

    var context = getContext();
    var collection = context.getCollection();

    updateUserList();

    function updateUserList() {
        // retrieve current user
        var getPlatformUserListQuery = 'SELECT * FROM root r where r.Type = "' + platformUserList.Type + '"';

        var isAccepted = collection.queryDocuments(
        collection.getSelfLink(),
        getPlatformUserListQuery,
        function (err, feed, options) {
            if (err) throw err;
            if (!feed || !feed.length) {
                // no document found, add one
                var created = collection.createDocument(collection.getSelfLink(),
                    platformUserList,
                    function (err, documentCreated) {
                        if (err) {
                            throw new Error('Error' + err.message);
                        }
                    });
                if (!created) throw "Unable to create platform user list, abort";
            } else {
                // document found, update and replace
                feed[0].Users.push(platformUserList.Users[0]);

                var accept = collection.replaceDocument(feed[0]._self, feed[0],
                function (err, docReplaced) {
                    if (err) throw "Unable to update platform user list";

                });

                if (!accept) throw "Unable to update platform user list, abort";
            }
        });

        if (!isAccepted) {
            throw new Error('The platform user list query was not accepted by the server.');
        }
    }
}