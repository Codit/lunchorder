module.exports = function (paths, dist) {
    var gulp = require("gulp"),
        debug = require("gulp-debug"),
        inject = require('gulp-inject'),
        series = require('stream-series'),
        insertLines = require('gulp-insert-lines');

    var input = {
        appIndex: paths.root + '/index.html',
        jsVendorRelease: paths.webroot + '/js/vendor/vendor.js',
        jsReleaseBundle: paths.webroot + '/app/bundle.js',
        jsAppRelease: paths.webroot + '/js/app.js',
        cssVendorRelease: paths.webroot + '/css/vendor/vendor.css',
        cssAppRelease: paths.webroot + '/css/app.css',
        jsJqueryPath: paths.webroot + '/js/vendor/jquery.js'
    }

    gulp.task('debug:inject-artifacts', ['assets-transform-debug', 'insert:systemJS:debug'], function () {

        var target = gulp.src(input.appIndex);

        // It's not necessary to read the files (will speed up things), we're only after their paths: 
        var jQueryJsSource = gulp.src([input.jsJqueryPath], { read: false });
        var vendorJsSource = gulp.src([paths.webroot + '/js/vendor/*.js', '!' + input.jsJqueryPath], { read: false });
        var appJsSource = gulp.src([paths.webroot + '/js/*.js'], { read: false });

        var vendorCssSource = gulp.src([paths.webroot + '/css/vendor/*.css'], { read: false });
        var appCssSource = gulp.src([paths.webroot + '/css/*.css'], { read: false });

        return target.pipe(inject(series(jQueryJsSource, vendorJsSource, appJsSource, vendorCssSource, appCssSource), { ignorePath: 'dist/', addRootSlash: false }))
        .pipe(insertLines({
                'before': /<\/body>$/,
                'lineBefore': `<!-- 2. Configure SystemJS only for debug, for production its included in the bundle -->
                    <script src="systemjs.config.js"></script>
                    <script>
                    System.import('app').catch(function(err){ console.error(err); });
                    </script>`
            }))
            .pipe(gulp.dest(paths.webroot));
    });

    gulp.task('release:inject-artifacts', ['assets-transform-release'], function () {

        var target = gulp.src(input.appIndex);

        var vendorJsSource = gulp.src([input.jsVendorRelease, input.jsReleaseBundle], { read: false });
        var appJsSource = gulp.src([input.jsAppRelease], { read: false });

        var vendorCssSource = gulp.src([input.cssVendorRelease], { read: false });
        var appCssSource = gulp.src([input.cssAppRelease], { read: false });

        return target.pipe(inject(series(vendorJsSource, appJsSource, vendorCssSource, appCssSource), { removeTags: true, ignorePath: 'dist/', addRootSlash: false }))
            .pipe(gulp.dest(paths.webroot));
    });

    gulp.task('insert:systemJS:debug', function () {
        gulp.src(input.appIndex)
            .pipe(insertLines({
                'before': /<\/body>$/,
                'lineBefore': `<!-- 2. Configure SystemJS only for debug, for production its included in the bundle -->
                    <script src="systemjs.config.js"></script>
                    <script>
                    System.import('app').catch(function(err){ console.error(err); });
                    </script>`
            }))
            .pipe(gulp.dest('dist'));
    });
}