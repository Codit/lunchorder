function storePushToken(pushTokenDocument) {
    var context = getContext();
    var collection = context.getCollection();

    var userId = pushTokenDocument.PushTokens[0].UserId;
    var pushToken = pushTokenDocument.PushTokens[0].Token;

    getPushTokenForUser(function(dbPushTokenDocument, needsUpdate) {
        if (needsUpdate) {
            updatePushToken(dbPushTokenDocument);
        }

        setUserPushToken();
        updateDbDocument(dbPushTokenDocument);
    });

    function setUserPushToken() {
        var getUserQuery = 'SELECT * FROM root r where r.id = "' + userId + '"';

        var isAccepted = collection.queryDocuments(
            collection.getSelfLink(),
            getUserQuery,
            function(err, feed, options) {
                if (err) throw err;
                if (!feed || !feed.length) {
                    throw "User (" + userId + ") does not exist";
                } else {
                    // document found, update and replace
                    feed[0].PushToken = pushToken;

                    var accept = collection.replaceDocument(feed[0]._self,
                        feed[0],
                        function(err, docReplaced) {
                            if (err) throw "Unable to update user (" + userId + ") push token";
                        });

                    if (!accept) throw "Unable to update user (" + userId + ") push token, abort";
                }
            });

        if (!isAccepted) {
            throw new Error('The user query was not accepted by the server.');
        }
    }

    function updateDbDocument(dbPushTokenDocument) {
        var accept = collection.replaceDocument(dbPushTokenDocument._self,
            dbPushTokenDocument,
            function(err, docReplaced) {
                if (err) throw "Unable to update push token document";

            });

        if (!accept) throw "Unable to update push token document, abort";
    }

    function updatePushToken(dbPushTokenDocument) {
        // remove existing 'old' record (if exists)
        for (var i = 0; i >= dbPushTokenDocument.PushTokens.length; i++) {
            if (dbPushTokenDocument.PushTokens[i].UserId === userId) {
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
            function(err, feed, options) {
                if (err) throw err;
                if (!feed || !feed.length) {
                    // create new document here
                    var accepted = collection.createDocument(collection.getSelfLink(),
                        pushTokenDocument,
                        function(err, documentCreated) {
                            if (err) {
                                throw new Error('Error' + err.message);
                            }
                            if (!documentCreated) {
                                throw "document could not be created";
                            }
                            cb(documentCreated, false);
                        });

                    if (!accepted) {
                        throw new Error('The create push token list query was not accepted by the server.');
                    }
                } else {
                    cb(feed[0], true);
                }
            });

        if (!isAccepted) {
            throw new Error('The get push token list query was not accepted by the server.');
        }
    }
}