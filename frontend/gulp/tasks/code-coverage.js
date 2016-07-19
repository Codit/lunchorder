module.exports = function (paths, dist) {
    var gulp = require('gulp');
    // used to upload to coveralls.io
    var coveralls = require('gulp-coveralls');
    // generates code coverage reports
    var istanbul = require('gulp-istanbul');
    // more debug info when using gulp
    var debug = require('gulp-debug');
    // run in sequence, should be supported in gulp@4.0
    var gulpSequence = require('gulp-sequence');
    // remaps code coverage back to typescript files instead of plain js files
    var remapIstanbul = require('remap-istanbul/lib/gulpRemapIstanbul');


// not working locally, use travis command instead from node cmd line
// https://github.com/markdalgleish/gulp-coveralls/pull/4
    var mappedPath = './coverage/lcov-frontend-remapped.info';
    gulp.task('remap-istanbul-frontend', function () {
        return gulp.src('./coverage/frontend/json/coverage-final.json')
        .pipe(debug())
            .pipe(remapIstanbul({
                fail: true,
                basePath: '.',
                reports: {
                    'lcovonly': mappedPath,
                },
            }))
            .pipe(debug());
    });

    gulp.task('coveralls', ['remap-istanbul-frontend']
        , function () {
            return gulp.src(mappedPath)
            .pipe(debug())
                .pipe(coveralls());
        });
};