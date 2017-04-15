function migrateUsers() {

    var context = getContext();
    var collection = context.getCollection();

    getUsers(function (userDocuments) {
        update(userDocuments);
    });

    function update(userDocuments) {
        for (var i = 0; i < userDocuments.length; i++) {
            if (userDocuments[i].Roles && userDocuments[i].Roles.length > 0 && userDocuments[i].Roles[0] == "prepay-admin") {
                userDocuments[i].Roles = [{
                    "kver": 2,
                    "id": "R_B36A5F40125153879FB0EFE8236BF4518EA674E0ADC466DC65DE2A4740015517",
                    "name": "prepay-admin"
                }];
            }
            userDocuments[i].UserId = userDocuments[i].id;
            userDocuments[i].kver = 2;

            var accept = collection.replaceDocument(userDocuments[i]._self, userDocuments[i],
                function (err, docReplaced) {
                    if (err) throw "Unable to update user";

                });

            if (!accept) throw "Unable to update user, abort";
        }
    }

    function getUsers(cb) {
        // retrieve current user
        var getUsersQuery = 'SELECT * FROM c where c.LockoutEnd != null';

        var isAccepted = collection.queryDocuments(
            collection.getSelfLink(),
            getUsersQuery,
            function (err, feed, options) {
                if (err) throw err;
                if (!feed || !feed.length) {
                    return;
                }

                cb(feed);
            });

        if (!isAccepted) {
            throw new Error('The user query was not accepted by the server.');
        }
    }
}