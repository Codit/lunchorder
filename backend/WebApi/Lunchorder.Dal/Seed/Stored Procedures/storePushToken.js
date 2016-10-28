function storePushToken(pushTokenDocument) {
    var context = getContext();
    var collection = context.getCollection();

    getPushTokenForUser(function (dbPushTokenDocument, needsUpdate) {
        

        if (needsUpdate) {
            updatePushToken(dbPushTokenDocument);
        }
        updateDbDocument(dbPushTokenDocument);
    });

    function updateDbDocument(dbPushTokenDocument) {
        var accept = collection.replaceDocument(dbPushTokenDocument._self, dbPushTokenDocument,
        function (err, docReplaced) {
            if (err) throw "Unable to update push token document";

        });

        if (!accept) throw "Unable to update push token document, abort";
    }

    function updatePushToken(dbPushTokenDocument) {
        // remove existing 'old' record (if exists)
        for (var i = 0; i >= dbPushTokenDocument.PushTokens.length; i++) {
            if (dbPushTokenDocument.PushTokens[i].UserId === pushTokenDocument.PushTokens[0].UserId) {
                dbPushTokenDocument.PushTokens.splice(i, 1);
                break;
            }
        }

        // insert new record
        dbPushTokenDocument.PushTokens.push(pushTokenDocument.PushTokens[0]);
    }

    function getPushTokenForUser(cb) {
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
                  if (!documentCreated) {
                          throw "EMPTY";
                  }
                  cb(documentCreated, false);
              });

                if (!accepted) {
                    throw new Error('The create push token list query was not accepted by the server.');
                }
            }
            else {
                cb(feed[0], true);
            }
        });

        if (!isAccepted) {
            throw new Error('The get push token list query was not accepted by the server.');
        }
    }
}