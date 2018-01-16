var webpackMerge = require('webpack-merge');
var devParams = require('./webpack.dev.params.js');
var prodConfig = require('./webpack.prod.js');

module.exports = webpackMerge(devParams, prodConfig, {
    devServer: {
        quiet: false,
        proxy: {
            '/api': {
                pathRewrite: { '^/api': '' } ,
                target: 'https://localhost:82/',
                secure: false
            }
        },
        https: true,
        historyApiFallback: true,
        stats: 'minimal',
        host: '0.0.0.0'
    }
});