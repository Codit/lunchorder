function updateUserBalance(userBalanceAudit) {

    var context = getContext();
    var collection = context.getCollection();

    getUser(function (userDocument) {
        updateBalance(userDocument);
        updateBalanceAudit();
    });

    function updateBalanceAudit() {
        // retrieve current user
        var getUserQuery = 'SELECT * FROM root r where r.UserId = "' + userBalanceAudit.UserId + '" and r.Type = "' + userBalanceAudit.Type + '"';

        var isAccepted = collection.queryDocuments(
        collection.getSelfLink(),
        getUserQuery,
        function (err, feed, options) {
            if (err) throw err;
            if (!feed || !feed.length) {
                // no document found, add one
                var created = collection.createDocument(collection.getSelfLink(),
                    userBalanceAudit,
                    function(err, documentCreated) {
                        if (err) {
                            throw new Error('Error' + err.message);
                        }
                    });
                if (!created) throw "Unable to create user balance audit, abort";
            } else {
                // document found, update and replace
                feed[0].Audits.push(userBalanceAudit.Audits[0]);

                var accept = collection.replaceDocument(feed[0]._self, feed[0],
                function (err, docReplaced) {
                if (err) throw "Unable to update user balance";

            });

                if (!accept) throw "Unable to update user balance, abort";
            }
        });

        if (!isAccepted) {
            throw new Error('The user query was not accepted by the server.');
        }
    }

    function updateBalance(userDocument) {
        userDocument.Balance = userDocument.Balance + userBalanceAudit.Audits[0].Amount;
        
        var accept = collection.replaceDocument(userDocument._self, userDocument,
            function (err, docReplaced) {
                if (err) throw "Unable to update user balance";

            });

        if (!accept) throw "Unable to update user balance, abort";

        context.getResponse().setBody(userDocument.Balance);
    }

    function getUser(cb) {
        // retrieve current user
        var getUserQuery = 'SELECT * FROM root r where r.id = "' + userBalanceAudit.UserId + '"';

        var isAccepted = collection.queryDocuments(
        collection.getSelfLink(),
        getUserQuery,
        function (err, feed, options) {
            if (err) throw err;
            if (!feed || !feed.length) {
                throw new Error('No user found for userId ' + userBalanceAudit.UserId);
            }

            cb(feed[0]);
        });

        if (!isAccepted) {
            throw new Error('The user query was not accepted by the server.');
        }
    }
}