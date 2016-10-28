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
                if (dbPushTokenDocument.PushTokens[i].UserId === userIds[0]) {
                    dbPushTokenDocument.PushTokens.splice(i, 1);
                }
            }
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