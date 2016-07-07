module.exports = function(paths, dist) {
var gulp = require("gulp"),
    debug = require("gulp-debug"),
    inject = require('gulp-inject'),
    series = require('stream-series');

var input = {
    appIndex: paths.root + '/index.html',
    jsVendorRelease: paths.webroot + '/js/vendor/vendor.js',
    jsAppRelease: paths.webroot + '/js/app.js',
    cssVendorRelease: paths.webroot + '/css/vendor/vendor.css',
    cssAppRelease: paths.webroot + '/css/app.css',
    jsJqueryPath: paths.webroot + '/js/vendor/jquery.js'
}

    gulp.task('debug:inject-artifacts', ['assets-transform-debug'], function () {

    var target = gulp.src(input.appIndex);

    // It's not necessary to read the files (will speed up things), we're only after their paths: 
    var jQueryJsSource = gulp.src([input.jsJqueryPath], { read: false });
    var vendorJsSource = gulp.src([paths.webroot  + '/js/vendor/*.js', '!'+input.jsJqueryPath], { read: false });
    var appJsSource = gulp.src([paths.webroot  + '/js/*.js'], { read: false });

    var vendorCssSource = gulp.src([paths.webroot + '/css/vendor/*.css'], { read: false });
    var appCssSource = gulp.src([paths.webroot + '/css/*.css'], { read: false });

    return target.pipe(inject(series(jQueryJsSource, vendorJsSource, appJsSource, vendorCssSource, appCssSource), { ignorePath: 'dist/', addRootSlash: false }))
      .pipe(gulp.dest(paths.webroot));
});

gulp.task('release:inject-artifacts', ['assets-transform-release'], function () {
    
    var target = gulp.src(input.appIndex);
        
        var vendorJsSource = gulp.src([input.jsVendorRelease], { read: false });
        var appJsSource = gulp.src([input.jsAppRelease], { read: false });

        var vendorCssSource = gulp.src([input.cssVendorRelease], { read: false });
        var appCssSource = gulp.src([input.cssAppRelease], { read: false });

        return target.pipe(inject(series(vendorJsSource, appJsSource, vendorCssSource, appCssSource), { removeTags: true, ignorePath: 'dist/', addRootSlash : false }))
        .pipe(gulp.dest(paths.webroot));
    });
}