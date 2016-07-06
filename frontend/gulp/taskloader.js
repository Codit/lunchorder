'use strict';


var fs = require('fs');
var gulp = require('gulp');

var paths = {
    webroot: "./dist",
    root: "./",
    modules: "node_modules"
};

var assetsClean = require('./tasks/assets-clean');
var assetsCopy = require('./tasks/assets-copy');
var assetsInject = require('./tasks/assets-inject');
var assetsTransform = require('./tasks/assets-transform');
assetsClean(paths);
assetsCopy(paths);
assetsInject(paths);
assetsTransform(paths);

gulp.task("debug", ['debug:inject-artifacts']);
gulp.task("release", ['release:inject-artifacts']);