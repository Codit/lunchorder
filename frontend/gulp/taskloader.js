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
var zipPackage = require('./tasks/zip-package');
csharpToTypescript();
assetsClean(paths);
assetsCopy(paths);
assetsInject(paths);
assetsTransform(paths);
zipPackage();

gulp.task("debug", ['debug:inject-artifacts']);
gulp.task("debug-watch", ['debug:inject-artifacts', 'watch:ts']);
gulp.task("release", ['release:inject-artifacts']);
gulp.task("test", ['debug:inject-artifacts']);

gulp.task("debug-package", gulpSequence('debug', 'zip-debug'));
gulp.task("release-package", gulpSequence('release', 'zip-release'));