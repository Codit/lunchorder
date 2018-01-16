var webpack = require('webpack');
var webpackMerge = require('webpack-merge');
var ExtractTextPlugin = require('extract-text-webpack-plugin');
var commonConfig = require('./webpack.common.js');
var helpers = require('./helpers');
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');
var ZipPlugin = require('zip-webpack-plugin');

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
      new ZipPlugin({
        path: '../_package',
        filename: 'package-release.zip'
      })
      , 
      new UglifyJsPlugin({
        uglifyOptions: {
            parallel: true, 
            ie8: false,
            output: {
                beautify: false,
                comments: false
             },
            mangle: {
                keep_fnames: true 
            }
        }
      }),
      new ExtractTextPlugin('[name].[hash].css'),
      new webpack.DefinePlugin({
          'process.env': {
              'ENV': JSON.stringify(ENV)
          }
      })
    ]
});
