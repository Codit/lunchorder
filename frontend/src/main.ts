import { browserDynamicPlatform } from '@angular/platform-browser-dynamic';
import { enableProdMode } from '@angular/core';
import { AppModule } from './app/app.module';

require('./assets/css/animate.scss');
require('./assets/css/custom.scss');
require('./assets/css/general.scss');
require('./assets/css/modal.scss');
require('./assets/css/style.scss');

if (process.env.ENV === 'production') {
    enableProdMode();
}
browserDynamicPlatform().bootstrapModule(AppModule);