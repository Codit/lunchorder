function storePushToken(pushTokenDocument) {
    var context = getContext();
    var collection = context.getCollection();

    getUserToken(function (dbPushTokenDocument, needsUpdate) {
        if (needsUpdate) {
            updatePushToken(dbPushTokenDocument);
        }
        updateDbDocument(dbPushTokenDocument);
    });

    function updateDbDocument(dbPushTokenDocument) {
        var accept = collection.replaceDocument(document._self, dbPushTokenDocument,
        function (err, docReplaced) {
            if (err) throw "Unable to update push token document";

        });

        if (!accept) throw "Unable to update push token document, abort";
    }

    function updatePushToken(dbPushTokenDocument) {
        if (dbPushTokenDocument.PushTokens) {
            throw "Push token document found but empty";
        }

        // remove existing 'old' record
        for (var i = 0; i >= dbPushTokenDocument.PushTokens.length; i++) {
            if (dbPushTokenDocument.PushTokens[i].UserId === pushTokenDocument.PushTokens[0].UserId) {
                dbPushTokenDocument.PushTokens.splice(i, 1);
                break;
            }
        }

        // insert new record
        dbPushTokenDocument.PushTokens.push(pushTokenDocument.PushTokens[0]);
    }

    function getUserToken(cb) {
        var getPushTokenDocQuery = 'SELECT * FROM root r where r.Type = "' + pushTokenDocument.Type + '"';

        var isAccepted = collection.queryDocuments(
        collection.getSelfLink(),
        getPushTokenDocQuery,
        function (err, feed, options) {
            if (err) throw err;
            if (!feed || !feed.length) {
                // create new document here
                var accepted = collection.createDocument(collection.getSelfLink(),
              pushTokenDocument,
              function (err, documentCreated) {
                  if (err) { throw new Error('Error' + err.message); }
                  cb(documentCreated[0], false);
              });

                if (!accepted) {
                    throw new Error('The create push token list query was not accepted by the server.');
                }
            }

            cb(feed[0], true);
        });

        if (!isAccepted) {
            throw new Error('The get push token list query was not accepted by the server.');
        }
    }
}