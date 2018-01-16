function deletePushToken(type, userIds) {
    var context = getContext();
    var collection = context.getCollection();

    getPushTokenDocument(function (dbPushTokenDocument) {
            removePushTokens(dbPushTokenDocument);
        updateDbDocument(dbPushTokenDocument);
    });

    function updateDbDocument(dbPushTokenDocument) {
        var accept = collection.replaceDocument(dbPushTokenDocument._self, dbPushTokenDocument,
        function (err, docReplaced) {
            if (err) throw "Unable to update push token document";

        });

        if (!accept) throw "Unable to update push token document, abort";
    }

    function removePushTokens(dbPushTokenDocument) {
        var i = dbPushTokenDocument.PushTokens.length;
        while (i--) {
            for (var j = 0; j < userIds.length; j++) {
                if (dbPushTokenDocument.PushTokens[i].UserId === userIds[j]) {
                    removePushTokenForUser(userIds[j]);
                    dbPushTokenDocument.PushTokens.splice(i, 1);
                }
            }
        }
    }

    function removePushTokenForUser(userId) {
        // get user document
        var getUserQuery = 'SELECT * FROM root r where r.id = "' + userId + '"';

        var isAccepted = collection.queryDocuments(
        collection.getSelfLink(),
        getUserQuery,
        function (err, feed, options) {
            if (err) throw err;
            if (!feed || !feed.length) {
                throw "There is a mismatch between the user (" + userId + ") push token and the push token document, abort";
            } else {
                // document found, update and replace
                feed[0].PushToken = '';

                var accept = collection.replaceDocument(feed[0]._self, feed[0],
                function (err, docReplaced) {
                    if (err) throw "Unable to update user (" + userId + ") push token";
                });

                if (!accept) throw "Unable to update user (" + userId + ") push token, abort";
            }
        });

        if (!isAccepted) {
            throw new Error('The user query was not accepted by the server.');
        }
    }

    function getPushTokenDocument(cb) {
        var getPushTokenDocQuery = 'SELECT * FROM root r where r.Type = "' + type + '"';

        var isAccepted = collection.queryDocuments(
        collection.getSelfLink(),
        getPushTokenDocQuery,
        function (err, feed, options) {
            if (err) throw err;
            if (!feed || !feed.length) {
                  throw new Error('There is no push token document in the database');
            }
            else {
                cb(feed[0]);
            }
        });

        if (!isAccepted) {
            throw new Error('The get push token list query was not accepted by the server.');
        }
    }
}