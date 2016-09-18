function updateUserPicture(userId, userPictureUrl) {

    var context = getContext();
    var collection = context.getCollection();

    modifyUserPicture();

    function modifyUserPicture() {
        // retrieve current user
        var getUserQuery = 'SELECT * FROM root r where r.id = "' + userId + '"';

        var isAccepted = collection.queryDocuments(
        collection.getSelfLink(),
        getUserQuery,
        function (err, feed, options) {
            if (err) throw err;
            if (!feed || !feed.length) {
                throw "Could not find user with id '" + userId + "'";
            } else {
                // document found, update and replace
                feed[0].Picture = userPictureUrl;

                var accept = collection.replaceDocument(feed[0]._self, feed[0],
                function (err, docReplaced) {
                    if (err) throw "Unable to update user picture";
                });

                if (!accept) throw "Unable to update user picture, abort";
            }
        });

        if (!isAccepted) {
            throw new Error('The get user query was not accepted by the server.');
        }
    }
}