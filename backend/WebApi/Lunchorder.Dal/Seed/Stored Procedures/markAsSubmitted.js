function updateUserBalance(vendorOrderHistoryId) {

    var context = getContext();
    var collection = context.getCollection();

    markAsSubmitted();

    function markAsSubmitted() {
        // retrieve current user
        var getVendorOrderHistory = 'SELECT * FROM root r where r.id = "' + vendorOrderHistoryId + '"';

        var isAccepted = collection.queryDocuments(
        collection.getSelfLink(),
        getVendorOrderHistory,
        function (err, feed, options) {
            if (err) throw err;
            if (!feed || !feed.length) {
                            throw new Error('There is no vendorOrderHistory to mark completed with id: ' + vendorOrderHistoryId);
            } else {
                // document found, update and replace
                feed[0].Submitted = true;

                var accept = collection.replaceDocument(feed[0]._self, feed[0],
                function (err, docReplaced) {
                    if (err) throw "Unable to mark vendorOrderHistory as completed";
                });

                if (!accept) throw "Unable to mark vendorOrderHistory as completed, abort";
            }
        });

        if (!isAccepted) {
            throw new Error('The vendorOrderHistory query was not accepted by the server.');
        }
    }
}