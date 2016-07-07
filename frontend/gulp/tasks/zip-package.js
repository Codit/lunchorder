module.exports = function (paths, dist) {
    var gulp = require("gulp"),
        path = require('path'),
        gnf = require('gulp-npm-files'),
        zip = require('gulp-zip'),
        debug = require('gulp-debug');

    gulp.task('prepare-package',
        function () {

            // copy all npm dependencies (excludes dev dep)
            return gulp.src(gnf(), { base: './' }).pipe(gulp.dest('./dist'));

        });

    gulp.task('zip-release', ['prepare-package'], function () {
        var options = { packageName: "package-release.zip", packagePath: path.join(__dirname, '../../_package') };

        return gulp.src('dist/**/*')
            .pipe(zip(options.packageName, { compress: true }))
            .pipe(gulp.dest(options.packagePath));
    });

    gulp.task('zip-debug', ['prepare-package'], function () {


        var options = { packageName: "package-debug.zip", packagePath: path.join(__dirname, '../../_package') };

        return gulp.src('dist/**/*')
            .pipe(zip(options.packageName, { compress: true }))
            .pipe(gulp.dest(options.packagePath));
    });
}