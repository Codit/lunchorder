module.exports = function (paths, dist) {
    var gulp = require("gulp"),
        debug = require("gulp-debug"),
        foreach = require('gulp-foreach'),
        rename = require('gulp-rename'),
        fs = require('fs');

    var input = {
        webConfig: paths.root + 'web.config'
    }

    gulp.task("copy:ts:params",
        function (cb) {
            return gulp.src([paths.root + '**/*.ts.dev', paths.root + '**/*.ts.params'])
                .pipe(debug())
                .pipe(foreach(function (stream, file) {
                    return stream
                        .pipe(rename(function (path) {
                            console.log('ext:' + path.extname)
                            // always overwrite when dev
                            if (path.extname === '.dev') {
                                path.extname = "";
                            }
                            else {
                                // if there is a dev file, ignore.
                                if (!fs.existsSync(path.dirname + "\\" + path.basename + ".dev")) {
                                    path.extname = "";
                                }
                            }
                        }))
                }))
                .pipe(debug())
                .pipe(gulp.dest('.'));
        });


    gulp.task("copy:webConfig",
        function (cb) {
            return gulp.src(input.webConfig)
                .pipe(gulp.dest(paths.webroot));
        });
}