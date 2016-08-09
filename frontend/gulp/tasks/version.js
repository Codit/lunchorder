module.exports = function (paths) {
    var gulp = require("gulp"),
        debug = require("gulp-debug"),
        version = require("../../package.json").version,
        replace = require('gulp-replace');

    gulp.task('version', function () {
        console.log(version);
        var filePath = paths.webroot + "/app/app.component.js";
        return gulp.src(filePath)
            .pipe(replace('%%VERSION%%', version))
            .pipe(gulp.dest(paths.webroot+"/app"));
    });
}