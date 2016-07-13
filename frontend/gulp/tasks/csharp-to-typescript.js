module.exports = function() {
var gulp = require("gulp"),
pocoGen = require('gulp-typescript-cs-poco'),
debug = require('gulp-debug')
concat = require('gulp-concat');

    gulp.task('csharp-to-typescript', function () {
        var pocoGenOptions = {
            prefixWithI: true,
            baseNamespace: 'app.domain.dto',
            dateTimeToDate: true,
            propertyNameResolver: function camelCaseResolver(propName) { return propName[0].toLowerCase() + propName.substring(1); }
        };

        return gulp.src('../backend/WebApi/Lunchorder.Domain/Dtos/**/*.cs')
            .pipe(debug())
            .pipe(concat('dtos.ts'))
                .pipe(pocoGen(pocoGenOptions))
                    .pipe(gulp.dest('app/domain/dto'));
    });
}