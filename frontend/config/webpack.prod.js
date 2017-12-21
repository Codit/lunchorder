var webpack = require('webpack');
var webpackMerge = require('webpack-merge');
var ExtractTextPlugin = require('extract-text-webpack-plugin');
var commonConfig = require('./webpack.common.js');
var helpers = require('./helpers');

const ENV = process.env.NODE_ENV = process.env.ENV = 'production';

module.exports = webpackMerge(commonConfig, {
    output: {
        path: helpers.root('dist'),
        publicPath: '/',
        filename: '[name].[hash].js',
        chunkFilename: '[id].[hash].chunk.js'
    },

    plugins: [
      new webpack.NoEmitOnErrorsPlugin(), 
      new webpack.optimize.UglifyJsPlugin({
          beautify: false,
            mangle: { screw_ie8 : true, keep_fnames: true },
            compress: { screw_ie8: true },
            comments: false
      }),
      new ExtractTextPlugin('[name].[hash].css'),
      new webpack.DefinePlugin({
          'process.env': {
              'ENV': JSON.stringify(ENV)
          }
      })
    ]
});
