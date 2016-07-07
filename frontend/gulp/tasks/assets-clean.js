module.exports = function(paths, dist) {
var gulp = require("gulp"),
    debug = require("gulp-debug"),
    del = require('del'),
    vinylPaths = require('vinyl-paths');

gulp.task("clean:dist", 
    function (cb) {
        return gulp.src(paths.webroot)
            .pipe(debug())
        .pipe(vinylPaths(del));
    });
}