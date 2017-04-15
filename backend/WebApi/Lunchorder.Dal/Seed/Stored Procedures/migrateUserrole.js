function migrateUserRole() {

    var context = getContext();
    var collection = context.getCollection();

    getUserWithRoles(function (userDocument) {
        updateRole(userDocument);
    });

    function updateRole(userDocument) {
        userDocument.Roles = [{
            "kver": 2,
            "id": "R_B36A5F40125153879FB0EFE8236BF4518EA674E0ADC466DC65DE2A4740015517",
            "name": "prepay-admin"
        }];

        var accept = collection.replaceDocument(userDocument._self, userDocument,
            function (err, docReplaced) {
                if (err) throw "Unable to update user role";

            });

        if (!accept) throw "Unable to update user role, abort";
    }

    function getUserWithRoles(cb) {
        // retrieve current user
        var getUsersWithRole = 'SELECT * FROM root r where ARRAY_CONTAINS(r.Roles, "prepay-admin")';

        var isAccepted = collection.queryDocuments(
            collection.getSelfLink(),
            getUsersWithRole,
            function (err, feed, options) {
                if (err) throw err;
                if (!feed || !feed.length) {
                    return;
                }

                cb(feed[0]);
            });

        if (!isAccepted) {
            throw new Error('The user role query was not accepted by the server.');
        }
    }
}