module.exports = function (paths, dist) {
    /// <binding BeforeBuild='debug' />
    "use strict";

    var gulp = require("gulp"),
        riraf = require("rimraf"),
        debug = require("gulp-debug"),
        gulpSequence = require('gulp-sequence'),
        gutil = require("gulp-util"),
        webpack = require("webpack"),
        webpackDebugConfig = require('../../config/webpack.dev.js'),
        webpackProdConfig = require('../../config/webpack.prod.js'),
        WebpackDevServer = require("webpack-dev-server");

    gulp.task('webpack-dev', ['copy:ts:params', "copy:webConfig"],
        function (callback) {
            // run webpack
            webpack(webpackDebugConfig, function (err, stats) {
                if (err) throw new gutil.PluginError("webpack", err);
                gutil.log("[webpack]", stats.toString({
                    // output options
                }));
                callback();
            })
        });

    gulp.task('webpack-prod', ['copy:ts:params', "copy:webConfig"],
        function (callback) {
            // run webpack
            webpack(webpackProdConfig,
                function (err, stats) {
                    if (err) throw new gutil.PluginError("webpack", err);
                    gutil.log("[webpack]",
                        stats.toString({
                            // output options

                        }));
                    callback();
                });
        });

    gulp.task("webpack-dev-server", ['copy:ts:params'], function (callback) {
        // Start a webpack-dev-server
        var myConfig = Object.create(webpackDebugConfig);
        var compiler = webpack(myConfig);

        new WebpackDevServer(compiler, {
            path: myConfig.output.path,
            publicPath: myConfig.output.publicPath,
            https: true,
            stats: {
                colors: true
            },
            host: '127.0.0.1',
            port: '3000',
            proxy: {
                '/api/*': {
                    target: 'https://127.0.0.1:1337/',
                    secure: false
                }
            },
            historyApiFallback: true
            // server and middleware options
            //"watch-poll": true
        }).listen(3000, "127.0.0.1", function (err) {
            if (err) throw new gutil.PluginError("webpack-dev-server", err);
            // Server listening
            gutil.log("[webpack-dev-server]", "https://127.0.0.1:3000");

            // keep the server alive or continue?
            // callback();
        });
    });
}