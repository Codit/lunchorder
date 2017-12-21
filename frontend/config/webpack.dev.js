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
        historyApiFallback: true,
        stats: 'minimal',
        // proxy: {
        //     '/api/*': {
        //         target: 'http://localhost:82/',
        //         secure: false
        //     }
        // }
    }
});