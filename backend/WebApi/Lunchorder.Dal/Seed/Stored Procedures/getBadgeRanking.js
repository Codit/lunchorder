function getBadgeRanking() {

    var context = getContext();
    var collection = context.getCollection();

    getRanking();

    function getRanking() {
        // retrieve current user
        var getUserQuery = 'SELECT c.UserId, c.UserName, c.Picture, ARRAY_LENGTH(c.Badges) as TotalBadges FROM c where c.Type = "User"';

        var isAccepted = collection.queryDocuments(
            collection.getSelfLink(),
            getUserQuery,
            function (err, feed, options) {
                if (err) throw err;
                if (!feed || !feed.length) {
                    throw "Could not find any users '";
                } else {
                    // documents found, update and replace
                    var sortedItems = feed.sort(function (item1, item2) {
                        return item2.TotalBadges - item1.TotalBadges;
                    });
                    var response = context.getResponse();
                    response.setBody(sortedItems.slice(0, 3));
                    return response;
                }
            });

        if (!isAccepted) {
            throw new Error('The get user query was not accepted by the server.');
        }
    }
}