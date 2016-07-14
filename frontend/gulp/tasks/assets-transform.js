module.exports = function(paths, dist) {
var gulp = require("gulp"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    debug = require("gulp-debug"),
    sass = require('gulp-sass'),
    del = require('del'),
    vinylPaths = require('vinyl-paths'),
    gulpSequence = require('gulp-sequence'),
    imagemin = require('gulp-imagemin'),
    rename = require('gulp-rename'),
    sass = require('gulp-sass'),
    jsmin = require('gulp-jsmin'),
    tsproject = require("tsproject"),
    systemJsBuilder = require('systemjs-builder');

var dist = {
    css: paths.webroot + '/css',
    cssAppConcat:  paths.webroot + '/css/app.css',
    js: paths.webroot + '/js',
    JsAppConcat:  paths.webroot + '/js/app.js',
    JsVendorConcat:  paths.webroot + '/js/vendor/vendor.js',
    cssVendorConcat:  paths.webroot + '/css/vendor/vendor.css',
    images: paths.webroot + "/css/images/"
}

var input = {
    vendorCss: paths.webroot + '/css/vendor/*.css',
    appJs: paths.webroot + '/js/*.js',
    appCss: paths.webroot + '/css/*.css',
    vendorJs: paths.webroot + '/js/vendor/*.js',
    bootstrapJs: paths.webroot + '/js/vendor/bootstrap.min.js',
    jqueryJs: paths.webroot + '/js/vendor/jquery.min.js',
    images: paths.webroot + '/css/images/*',
    distCss: paths.webroot + '/css/**/*.css',
    distSass: paths.webroot + '/css/**/*.scss',
    distJs: paths.webroot + '/js/**/*.js'
}

// todo add uglify later
gulp.task("assets-transform-release", gulpSequence(['assets-copy-release'], ['css:app:compile:sass', 'transpile:ts:release'], 'bundle:systemJs', 'minify', 'concat'));
gulp.task("assets-transform-debug", gulpSequence(['assets-copy-debug'], 'css:app:compile:sass', 'transpile:ts:debug'));

gulp.task('minify', ['minify:images', 'minify:css', 'minify:js']);
gulp.task('minify:images', function() {
    return gulp.src(input.images)
		.pipe(imagemin())
		.pipe(gulp.dest(dist.images))
});

gulp.task('bundle:systemJs', function() {
 // optional constructor options
    // sets the baseURL and loads the configuration file
    var builder = new systemJsBuilder('', './systemjs.config.js');

    /*
       the parameters of the below buildStatic() method are:
           - your transcompiled application boot file (the one wich would contain the bootstrap(MyApp, [PROVIDERS]) function - in my case 'dist/app/boot.js'
           - the output (file into which it would output the bundled code)
           - options {}
    */
    return builder
        .buildStatic('dist/app/main.js', 'dist/app/bundle.js', { minify: true, sourceMaps: false, mangle: true, rollup: false })
        .then(function() {
            console.log('Build complete');
        })
        .catch(function(err) {
            console.log('Build error');
            console.log(err);
        });
});

gulp.task('transpile:ts:release', function() {
    	return tsproject.src("tsconfig.json", {
    		compilerOptions: {
    			"inlineSourceMap": false,
    			"inlineSources": false
    		}
    	})
        .pipe(gulp.dest("."));
    });

gulp.task('transpile:ts:debug', function () {
    	return tsproject.src("tsconfig.json")
        .pipe(debug())
			.pipe(gulp.dest("."));
    });

gulp.task('minify:css', function() {
    gulp.src(input.distCss)
        .pipe(debug())
        .pipe(cssmin())
        .pipe(rename({suffix: '.min'}))
        .pipe(gulp.dest(dist.css));

        // cleanup
        gulp.src([input.distCss, '!*min.css'])
        .pipe(debug())
   .pipe(vinylPaths(del));
});

gulp.task('minify:js', function() {
    gulp.src(input.distJs)
        .pipe(debug())
        .pipe(jsmin())
        .pipe(rename({suffix: '.min'}))
        .pipe(gulp.dest(dist.js));

        // cleanup
        gulp.src([input.distJs, '!*min.css'])
        .pipe(debug())
   .pipe(vinylPaths(del));
});

gulp.task('css:app:compile:sass', function() {
    var result = gulp.src(input.distSass)
        .pipe(debug())
        .pipe(sass().on('error', sass.logError))
        .pipe(gulp.dest(dist.css));

           // cleanup
        gulp.src(input.distSass)
        .pipe(debug())
   .pipe(vinylPaths(del));
   return result;
});

gulp.task("concat", ['concat:css', 'concat:js']);
gulp.task("concat:css", ['concat:css:app', 'concat:css:vendor']);
gulp.task("concat:js", ['concat:js:app', 'concat:js:vendor']);

gulp.task("concat:css:app", function () {
    var result = gulp.src(input.appCss)
        .pipe(debug())
        .pipe(concat(dist.cssAppConcat))
        .pipe(gulp.dest("."));

// cleanup
        gulp.src([input.appCss, '!' + dist.cssAppConcat])
        .pipe(debug())
   .pipe(vinylPaths(del));

    return result;
});

gulp.task("concat:css:vendor", function () {
    var result = gulp.src(input.vendorCss)
        .pipe(debug())
        .pipe(concat(dist.cssVendorConcat))
        .pipe(gulp.dest("."));

// cleanup
        gulp.src([input.vendorCss, '!' + dist.cssVendorConcat])
        .pipe(debug())
   .pipe(vinylPaths(del));

    return result;
});

gulp.task("concat:js:app", function () {
    var result = gulp.src(input.appJs)
        .pipe(debug())
        .pipe(concat(dist.JsAppConcat))
        .pipe(gulp.dest("."));

// cleanup
        gulp.src([input.appJs, '!' + dist.JsAppConcat])
        .pipe(debug())
   .pipe(vinylPaths(del));

    return result;
});

gulp.task("concat:js:vendor", function () {
    // add files where order is important first, then add other files (dupes should be ignored)
    var sourceItems = [input.jqueryJs, input.bootstrapJs, input.vendorJs];
    var result = gulp.src(sourceItems)
        .pipe(debug())
        .pipe(concat(dist.JsVendorConcat))
        .pipe(gulp.dest("."));

// cleanup
    var cleanSources = sourceItems;
    cleanSources.push('!'+dist.JsVendorConcat);
        gulp.src(cleanSources)
        .pipe(debug())
   .pipe(vinylPaths(del));

    return result;
});
}