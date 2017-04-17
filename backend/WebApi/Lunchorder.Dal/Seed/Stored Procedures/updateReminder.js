function updateReminder(reminder, userId) {

    var context = getContext();
    var collection = context.getCollection();
    var action = reminder.Action;

    delete reminder.Action;

    getUser(function (userDocument) {
        if (action === "AddOrUpdate") {
            updateUserReminder(userDocument);
        }
        else if (action === "Delete") {
            deleteUserReminder(userDocument);
        } else {
            throw "Action is missing";
        }
        updateUserDocument(userDocument);
    });

    function deleteUserReminder(userDocument) {
        // easy peasy when there is no reminder yet
        if (!userDocument.Reminders) {
            userDocument.Reminders = [];
        } else {
            var foundIndex = null;
            // lookup if there is an existing reminder type
            for (var i = 0; i < userDocument.Reminders.length; i++) {
                if (userDocument.Reminders[i].Type === reminder.Type) {
                    foundIndex = i;
                }
            }
            if (foundIndex >= 0) {
                userDocument.Reminders.splice(foundIndex, 1);
            }
        }
        userDocument.PushToken = null;
    };

    function updateUserReminder(userDocument) {
        // easy peasy when there is no reminder yet
        if (!userDocument.Reminders) {
            userDocument.Reminders = [];
            userDocument.Reminders.push(reminder);
        } else {
            var foundIndex;
            // lookup if there is an existing reminder type
            for (var i = 0; i < userDocument.Reminders.length; i++) {
                if (userDocument.Reminders[i].Type === reminder.Type) {
                    foundIndex = i;
                }
            }
            if (foundIndex >= 0) {
                userDocument.Reminders.splice(foundIndex, 1);
            }
            userDocument.Reminders.push(reminder);
        }
    };

    function updateUserDocument(userDocument) {
        var accept = collection.replaceDocument(userDocument._self, userDocument,
            function (err, docReplaced) {
                if (err) throw "Unable to update user reminder";

            });

        if (!accept) throw "Unable to update user reminder, abort";

        context.getResponse().setBody(userDocument.Balance);
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
}