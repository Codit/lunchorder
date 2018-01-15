var webpackMerge = require('webpack-merge');

module.exports = {
    module: {
        loaders: [
            {
                "test": /config\.service\.ts$/,
                "loader": "string-replace-loader",
                "query": {
                    "multiple": [
                        /* authentication */
                        { "search": "____configTenant____", "replace": "7517bc42-bcf8-4916-a677-b5753051f846", strict: true },
                        { "search": "____configClientId____", "replace": "f2477cb6-a5a2-40d1-8ba2-736ffd224519", strict: true },
                        { "search": "____activeDirectoryEnabled____", "replace": "true", strict: true },
                        { "search": "____usernamePasswordEnabled____", "replace": "false", strict: true },

                        /* demo */
                        { "search": "____isDemo____", "replace": "false", strict: true },
                        { "search": "____demoAdmin____", "replace": "", strict: true },
                        { "search": "____demoAdminPass____", "replace": "", strict: true },
                        { "search": "____demoUser____", "replace": "", strict: true },
                        { "search": "____demoUserPass____", "replace": "", strict: true },

                        /* various */
                        { "search": "____allowWeekendOrders____", "replace": "false", strict: true }
                    ]
                }
            },
            {
                "test": /app\.footer\.html$/,
                "loader": "string-replace-loader",
                "query": {
                    "multiple": [
                        { "search": "____version____", "replace": "1.0.0.0", strict: true }
                    ]
                }
            }
        ]
    }
};