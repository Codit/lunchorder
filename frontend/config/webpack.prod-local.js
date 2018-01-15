var webpackMerge = require('webpack-merge');
var devParams = require('./webpack.dev.params.js');
var prodConfig = require('./webpack.prod.js');

module.exports = webpackMerge(devParams, prodConfig);