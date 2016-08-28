function addUserOrder(vendorOrderId, docDbVendorOrderHistoryEntries, docDbUserOrderHistory, lastOrder) {
    if (!docDbVendorOrderHistoryEntries || !docDbVendorOrderHistoryEntries.length) {
        throw new Error('There are no entries to insert');
    }

    var userId = docDbVendorOrderHistoryEntries[0].UserId;
    var context = getContext();
    var collection = context.getCollection();
    var vendorOrderHistoryQuery = 'SELECT * FROM root r where r.id = "' + vendorOrderId + '"';

    

    getUser(function (userDocument) {
        updateUserBalance(userDocument);
        updateUserOrders(userDocument);
        updateUserDocument(userDocument);
        addVendorOrderHistory();
        addUserOrderHistory();
    });

    function updateUserBalance(userDocument) {
        userDocument.Balance = userDocument.Balance - docDbUserOrderHistory.FinalPrice;
        userDocument.Balance = userDocument.toFixed(2);
        if (userDocument.Balance < 0) {
            throw "Not enough money in your wallet";
        }
    }

    function updateUserOrders(userDocument) {
        // we only keep track of the last 10 orders for a user
        if (userDocument.Last5Orders && userDocument.Last5Orders.length >= 5) {
            userDocument.Last5Orders.shift();
        }

        // insert at beginning
        userDocument.Last5Orders.unshift(lastOrder);
    };

    function updateUserDocument(document) {
        var accept = collection.replaceDocument(document._self, document,
            function (err, docReplaced) {
                if (err) throw "Unable to update user";

            });

        if (!accept) throw "Unable to update user, abort";
    }

    function getUser(cb) {
        // retrieve current user
        var getUserQuery = 'SELECT * FROM root r where r.id = "' + userId + '"';

        var isAccepted = collection.queryDocuments(
        collection.getSelfLink(),
        getUserQuery,
        function (err, feed, options) {
            if (err) throw err;
            if (!feed || !feed.length) {
                throw new Error('No user found for userId ' + userId);
            }

            cb(feed[0]);
        });

        if (!isAccepted) {
            throw new Error('The user query was not accepted by the server.');
        }
    }

    /* Creates a user order history record */
    function addUserOrderHistory() {
        var accepted = collection.createDocument(collection.getSelfLink(),
              docDbUserOrderHistory,
              function (err, documentCreated) {
                  if (err) { throw new Error('Error' + err.message); }
                  
              });

        if (!accepted) {
            throw new Error('The add user order history query was not accepted by the server.');
        }
    }

    function addVendorOrderHistory() {
        var isAccepted = collection.queryDocuments(
        collection.getSelfLink(),
        vendorOrderHistoryQuery,
        function (err, feed, options) {
            if (err) throw err;
            if (!feed || !feed.length) {
                throw new Error('No existing vendor order document');
            }

            docDbVendorOrderHistoryEntries.forEach(function (v) { feed[0].Entries.push(v) }, this);
            updateVendorOrderHistory(feed[0]);

            context.getResponse().setBody(true);
        });

        if (!isAccepted) throw new Error('The query was not accepted by the server.');
    }

    function updateVendorOrderHistory(document) {
        var accept = collection.replaceDocument(document._self, document,
            function (err, docReplaced) {
                if (err) throw "Unable to update VendorOrderHistory";

            });

        if (!accept) throw "Unable to update VendorOrderHistory, abort";
    }
}