var webpackMerge = require('webpack-merge');
var ExtractTextPlugin = require('extract-text-webpack-plugin');
var commonConfig = require('./webpack.common.js');
var devParamsConfig = require('./webpack.dev.params.js');
var helpers = require('./helpers');

module.exports = webpackMerge(devParamsConfig, commonConfig, {
    devtool: 'source-map',

    output: {
        path: helpers.root('dist'),
        publicPath: '/',
        filename: '[name].js',
        chunkFilename: '[id].chunk.js'
    },

    plugins: [
      new ExtractTextPlugin('[name].css')
    ],

    devServer: {
        quiet: false,
        proxy: {
            '/api': {
                // pathRewrite: { '^/api': '' } ,
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