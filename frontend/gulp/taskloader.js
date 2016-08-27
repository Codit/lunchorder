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
var assetsInject = require('./tasks/assets-inject');
var assetsTransform = require('./tasks/assets-transform');
var csharpToTypescript = require('./tasks/csharp-to-typescript');
var codeCoverage = require('./tasks/code-coverage');
var version = require('./tasks/version');
var zipPackage = require('./tasks/zip-package');
var webpack = require('./tasks/webpack');
csharpToTypescript();
assetsClean(paths);
assetsCopy(paths);
assetsInject(paths);
assetsTransform(paths);
zipPackage();
codeCoverage();
version(paths);
webpack(paths);

gulp.task("debug", ['debug:inject-artifacts']);
gulp.task("debug-watch", gulpSequence('debug:inject-artifacts', 'watch:ts'));
gulp.task("release", ['release:inject-artifacts']);
gulp.task("test", ['debug:inject-artifacts']);

gulp.task("debug-package", gulpSequence('zip-debug'));
gulp.task("release-package", gulpSequence('release', 'zip-release'));