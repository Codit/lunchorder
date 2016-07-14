/**
 * System configuration for Angular 2 samples
 * Adjust as necessary for your application needs.
 */
(function(global) {
  // map tells the System loader where to look for things
  var map = {
    'app':                        'app', // 'dist',
    'moment':                     'node_modules/moment',
    'angular2-moment':            'node_modules/angular2-moment',
    '@angular':                   'node_modules/@angular',
    // 'angular2-in-memory-web-api': 'node_modules/angular2-in-memory-web-api',
    "zone":                       'node_modules/zone.js/dist',
    "crypto":                     'node_modules/crypto',
    'reflect-metadata':           'node_modules/reflect-metadata',
    'rxjs':                       'node_modules/rxjs',
    'angular2-adal':              'node_modules/angular2-adal',
    'adal':                       'node_modules/adal-angular/lib'
  };
  // packages tells the System loader how to load when no filename and/or no extension
  var packages = {
    
    // 'angular2-in-memory-web-api': { main: 'index.js', defaultExtension: 'js' },
    'zone': { main: 'zone.js', defaultExtension: 'js' },
    'reflect-metadata': { main: 'Reflect.js', defaultExtension: 'js' },
    'crypto': { main: 'sha1.js', defaultExtension: 'js' },
    'moment':  { main: 'moment.js', defaultExtension: 'js' },
    'rxjs':  { main: 'Rx.js', defaultExtension: 'js' },
    'angular2-moment': { defaultExtension: 'js' },
	  'angular2-adal': { main: 'core.js', defaultExtension: 'js' },
    'adal': { main: 'adal.js', defaultExtension: 'js' },
    'app':                        { main: 'main.js',  defaultExtension: 'js' }
  };
  var ngPackageNames = [
    'common',
    'compiler',
    'core',
    'forms',
    'http',
    'platform-browser',
    'platform-browser-dynamic',
    'router',
    // 'router-deprecated',
    'upgrade',
  ];
  // Individual files (~300 requests):
  function packIndex(pkgName) {
    packages['@angular/'+pkgName] = { main: 'index.js', defaultExtension: 'js' };
  }
  // Bundled (~40 requests):
  function packUmd(pkgName) {
    packages['@angular/'+pkgName] = { main: '/bundles/' + pkgName + '.umd.js', defaultExtension: 'js' };
  }
  // Most environments should use UMD; some (Karma) need the individual index files
  var setPackageConfig = System.packageWithIndex ? packIndex : packUmd;
  // Add package entries for angular packages
  ngPackageNames.forEach(setPackageConfig);
  var config = {
    map: map,
    packages: packages,
    defaultJSExtensions: true
  };
  System.config(config);
})(this);