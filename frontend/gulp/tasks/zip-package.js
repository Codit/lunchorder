module.exports = function (paths, dist) {
    var gulp = require("gulp"),
        path = require('path'),
        zip = require('gulp-zip'),
        debug = require('gulp-debug');

    gulp.task('zip-release', function () {
        var options = { packageName: "package-release.zip", packagePath: path.join(__dirname, '../../_package') };

        return gulp.src('dist/**/*')
            .pipe(zip(options.packageName, { compress: true }))
            .pipe(gulp.dest(options.packagePath));
    });

    gulp.task('zip-debug', function () {
        var options = { packageName: "package-debug.zip", packagePath: path.join(__dirname, '../../_package') };

        return gulp.src('dist/**/*')
            .pipe(zip(options.packageName, { compress: true }))
            .pipe(gulp.dest(options.packagePath));
    });
}