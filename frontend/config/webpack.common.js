var webpack = require('webpack');
var HtmlWebpackPlugin = require('html-webpack-plugin');
var ExtractTextPlugin = require('extract-text-webpack-plugin');
var helpers = require('./helpers');
const path = require('path');

// const sassLoaders = [
//   'css-loader',
//   'postcss-loader',
//   'sass-loader?indentedSyntax=sass&includePaths[]=' + path.resolve(__dirname, './src')
// ]

module.exports = {
    entry: {
        // 'bootstrap': 'bootstrap',
        'polyfills': './src/polyfills.ts',
        'vendor': './src/vendor.ts',
        'app': './src/main.ts',
    },

    resolve: {
        extensions: ['', '.js', '.ts', '.css']
    },

    module: {
        loaders: [
          {
              test: /\.ts$/,
              loaders: ['ts', 'angular2-template-loader']
          },
          {
              test: /\.html$/,
              loader: 'html'
          },
          {
              test: /\.(png|jpe?g|gif|ico)$/,
              loader: 'file?name=assets/[name].[hash].[ext]'
          },
          {
                test: /\.css$/,
                loader: ExtractTextPlugin.extract("style-loader", "css-loader")
            },
          {
              test: /\.scss$/,
              loaders: ["style", "css", "sass"],
              loader: ExtractTextPlugin.extract('style', 'css?sourceMap')
          },

        //   {test: /\.scss/, loader: 'style!css!sass?includePaths[]=' +         (path.resolve(__dirname, "./node_modules"))},
    {test: /\.woff(\?v=[0-9]\.[0-9]\.[0-9])?$/, loader: "url-loader?limit=10000&mimetype=application/font-woff&name=fonts/[name].[ext]" },
    {test: /\.(ttf|eot|svg)(\?v=[0-9]\.[0-9]\.[0-9])?$/, loader: "file-loader" },
          
        //   {
        //   test: /\.scss$/,
        //     loader: ExtractTextPlugin.extract('style-loader', sassLoaders.join('!'))
        //     },
        //    {
//             test: /\.woff(\?v=\d+\.\d+\.\d+)?$/,
//   loader: "url?limit=10000&mimetype=application/font-woff"
//   }
, {
      test: /\.woff2(\?v=\d+\.\d+\.\d+)?$/,
      loader: "url?limit=10000&mimetype=application/font-woff"
  }
// , {
//   test: /\.ttf(\?v=\d+\.\d+\.\d+)?$/,
//       loader: "url?limit=10000&mimetype=application/octet-stream"
//   }, {
//       test: /\.eot(\?v=\d+\.\d+\.\d+)?$/,
//       loader: "file"
//   }, {
//   test: /\.svg(\?v=\d+\.\d+\.\d+)?$/,
//       loader: "url?limit=10000&mimetype=image/svg+xml"
//   },
//           {
//             test: require.resolve('../node_modules/wow/dist/wow.js'), 
//             loader: 'exports?this.WOW'
//             }
        ]
    },

    plugins: [
      new webpack.optimize.CommonsChunkPlugin({
          name: ['app', 'vendor', 'polyfills']
      }),

      new HtmlWebpackPlugin({
          template: 'src/index.html'
      }),

      new webpack.ProvidePlugin({
          jQuery: 'jquery',
          $: 'jquery',
          jquery: 'jquery'
      })
    ]
};