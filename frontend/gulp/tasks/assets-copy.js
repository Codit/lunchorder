module.exports = function (paths, dist) {
    var gulp = require("gulp"),
        debug = require("gulp-debug"),
        gulpSequence = require('gulp-sequence'),
        gnf = require('gulp-npm-files'),
        foreach = require('gulp-foreach'),
        rename = require('gulp-rename'),
        replace = require('gulp-replace'),
        fs = require('fs');

    var input = {
        images: paths.root + 'css/images/**/*.{png,gif,jpg,jpeg,svg}',
        cssApp: paths.root + 'css/**/*.scss',
        cssBootstrap: paths.modules + '/bootstrap/dist/css/bootstrap.css',
        cssOwlCarousel: paths.modules + '/owlcarousel-pre/owl-carousel/owl.carousel.css',
        cssOwlCarouselTheme: paths.modules + '/owlcarousel-pre/owl-carousel/owl.theme.css',
        fontsApp: paths.root + 'css/fonts/**/*',
        fontsFontAwesome: paths.modules + '/font-awesome/fonts/**/*',
        html: paths.root + 'app/**/*.html',
        js: paths.root + 'js/*.js',
        jsJquery: paths.modules + '/jquery/dist/jquery.js',
        jsBootstrap: paths.modules + '/bootstrap/dist/js/bootstrap.js',
        jsWow: paths.modules + '/wow/dist/wow.js',
        jsOwlCarousel: paths.modules + '/owlcarousel-pre/owl-carousel/owl.carousel.js',
        jsZone: paths.modules + '/zone.js/dist/zone.js',
        jsReflect: paths.modules + '/reflect-metadata/Reflect.js',
        jsSystemJs: paths.modules + '/systemjs/dist/system.src.js',
        systemJsConfig: paths.root + 'systemjs.config.js',
        webConfig: paths.root + 'web.config'
    }

    var dist = {
        images: paths.webroot + "/css/images/",
        cssApp: paths.webroot + "/css/",
        cssVendor: paths.webroot + "/css/vendor/",
        jsVendor: paths.webroot + "/js/vendor",
        fonts: paths.webroot + "/css/fonts",
        html: paths.webroot + 'app/html'
    }

    gulp.task('assets-copy', gulpSequence('clean:dist', ['copy:ts:params', 'copy:css', 'copy:fonts', 'copy:html', 'copy:images', 'copy:js', 'copy:systemJsConfig', 'copy:webConfig', 'copy:npm:dependencies']));

    gulp.task('copy:css', ['copy:css:vendor', 'copy:css:app']);
    gulp.task('copy:js', ['copy:js:vendor']);//, 'copy:js:app']);

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

    gulp.task("copy:css:vendor",
        function (cb) {
            return gulp.src([input.cssOwlCarousel, input.cssOwlCarouselTheme, input.cssBootstrap])
                .pipe(gulp.dest(dist.cssVendor));
        });

    gulp.task("copy:css:app",
        function (cb) {
            return gulp.src([input.cssApp])
                .pipe(gulp.dest(dist.cssApp));
        });

    gulp.task("copy:js:vendor",
        function (cb) {
            return gulp.src([input.jsJquery, input.jsBootstrap, input.jsWow, input.jsOwlCarousel, input.jsZone, input.jsReflect, input.jsSystemJs, input.js])
                .pipe(gulp.dest(dist.jsVendor));
        });

    gulp.task('copy:npm:dependencies',
        function () {
            // copy all npm dependencies (excludes dev dep)
            return gulp.src(gnf(), { base: './' }).pipe(gulp.dest('./dist'));
        });

    // gulp.task("copy:js:app",
    //     function (cb) {
    //         // todo compile typescript.
    //         return null;
    //         // return gulp.src([input.cssApp])
    //         //        .pipe(gulp.dest(dist.jsApp));
    //     });

    gulp.task("copy:html",
        function (cb) {
            return gulp.src(input.html)
                .pipe(gulp.dest(dist.html));
        });


    gulp.task("copy:systemJsConfig",
        function (cb) {
            return gulp.src(input.systemJsConfig)
                .pipe(gulp.dest(paths.webroot));
        });

    gulp.task("copy:webConfig",
        function (cb) {
            return gulp.src(input.webConfig)
                .pipe(gulp.dest(paths.webroot));
        });


    gulp.task("copy:images",
        function (cb) {
            return gulp.src(input.images)
                .pipe(gulp.dest(dist.images));
        });

    gulp.task("copy:fonts", function (cb) {
        return gulp.src([input.fontsApp, input.fontsFontAwesome])
            .pipe(gulp.dest(dist.fonts));
    });
}