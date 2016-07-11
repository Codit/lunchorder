import { bootstrap }    from '@angular/platform-browser-dynamic';
import { AdalService } from 'angular2-adal/core';

import { AppComponent } from './app.component';
import { ConfigService } from './services/configService';

bootstrap(AppComponent, [AdalService, ConfigService]);