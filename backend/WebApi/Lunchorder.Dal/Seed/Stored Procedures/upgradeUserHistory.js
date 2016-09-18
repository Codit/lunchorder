function upgradeUserHistory() {

    var context = getContext();
    var collection = context.getCollection();

    modifyUserPicture();

    function modifyUserPicture() {
        // retrieve current user
        var getUsersQuery = 'SELECT * FROM root r where r.Type = "PlatformUserList"';

        var isAccepted = collection.queryDocuments(
        collection.getSelfLink(),
        getUsersQuery,
        function (err, feed, options) {
            if (err) throw err;
            if (!feed || !feed.length) {
                throw "Could not find platform user list";
            } else {
                feed[0].Users.forEach(function (user) {
                    // get the actual user
                    var getUserQuery = 'SELECT * FROM root r where r.id = "' + user.UserId + '"';

                    isAccepted = collection.queryDocuments(
                        collection.getSelfLink(),
                        getUserQuery,
                        function (err, userFeed, options) {
                            if (err) throw err;
                            if (!userFeed || !userFeed.length) {
                                throw "Could not find user with id '" + user.UserId + "'";
                            }
                            // don't update if already has item
                            if (!userFeed[0].Last5BalanceAuditItems || userFeed[0].Last5BalanceAuditItems.length === 0) {
                                userFeed[0].Last5BalanceAuditItems = [];

                                var getUserBalanceAuditQuery = 'SELECT * FROM root r where r.Type="UserBalanceAudit" and r.UserId="' + userFeed[0].id + '"';

                                isAccepted = collection.queryDocuments(
                                    collection.getSelfLink(),
                                    getUserBalanceAuditQuery,
                                    function (err, auditFeed, options) {
                                        if (err) throw err;

                                        if (auditFeed || auditFeed.length) {
                                            if (auditFeed[0] && auditFeed[0].Audits) {
                                                var totalAudits = auditFeed[0].Audits.length - 1;
                                                var maxIterations = totalAudits - 5;
                                                for (var i = totalAudits; i >= 0 && i !== maxIterations; i--) {
                                                    userFeed[0].Last5BalanceAuditItems.unshift(auditFeed[0].Audits[i]);
                                                }
                                            }
                                        }
                                        else {
                                            throw "Could not find user audit history with userid '" + user.UserId + "'";
                                        }

                                        // update user
                                        var accept = collection.replaceDocument(userFeed[0]._self,
                                            userFeed[0],
                                            function (err, docReplaced) {
                                                if (err) throw "Unable to update user: " + user.UserId;
                                            });

                                        if (!accept) throw "Unable to update user, abort";
                                    });

                                
                            }
                        });


                });

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