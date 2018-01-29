function getOrCreateVendorOrderHistory(vendorOrderHistory) {
    var context = getContext();
    var collection = context.getCollection();
    var vendorOrderHistoryQuery = 'SELECT * FROM root r where r.Type = "' + vendorOrderHistory.Type +
        '" and r.VendorId = "' + vendorOrderHistory.VendorId +
        '" and r.OrderDate = "' + vendorOrderHistory.OrderDate + '"';

    // Query documents and take 1st item.
    var isAccepted = collection.queryDocuments(
        collection.getSelfLink(),
        vendorOrderHistoryQuery,
        function (err, feed, options) {
            if (err) throw err;

            // Check the feed and if empty, set the body to 'no docs found', 
            // else take 1st element from feed
            if (!feed || !feed.length) {
                // no document found, add one
                var created = collection.createDocument(collection.getSelfLink(),
             vendorOrderHistory,
             function (err, documentCreated) {
                 if (err) { throw new Error('Error' + err.message); }
                 context.getResponse().setBody(documentCreated.id);
             });
                if (!created) return;
            }
            else context.getResponse().setBody(feed[0].id);
        });

    if (!isAccepted) throw new Error('The query was not accepted by the server.');
}
