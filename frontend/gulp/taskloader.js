'use strict';


var fs = require('fs');
var gulp = require('gulp');
var gulpSequence = require('gulp-sequence');

var paths = {
    webroot: "./dist",
    root: "./",
    modules: "node_modules"
};

var assetsClean = require('./tasks/assets-clean');
var assetsCopy = require('./tasks/assets-copy');
var csharpToTypescript = require('./tasks/csharp-to-typescript');
var codeCoverage = require('./tasks/code-coverage');
var version = require('./tasks/version');
var zipPackage = require('./tasks/zip-package');
var webpack = require('./tasks/webpack');
csharpToTypescript();
assetsClean(paths);
assetsCopy(paths);
zipPackage();
codeCoverage();
version(paths);
webpack(paths);

gulp.task("debug", gulpSequence('clean:dist', ['webpack-dev']));
gulp.task("debug-watch", gulpSequence('clean:dist',['webpack-dev-server']));
gulp.task("release", gulpSequence('clean:dist',['webpack-prod']));
gulp.task("test", gulpSequence('clean:dist',['copy:ts:params']));

gulp.task("debug-package", gulpSequence('debug', 'zip-debug'));
gulp.task("release-package", gulpSequence('release', 'zip-release'));