import { bootstrap }    from '@angular/platform-browser-dynamic';
import { AdalService } from 'angular2-adal/core';
import { HTTP_PROVIDERS, BaseRequestOptions, RequestOptions } from '@angular/http';
import { AppComponent } from './app.component';
import { ConfigService } from './services/configService';
import { AccountService } from './services/accountService';
import { AuthRequestOptions } from './helpers/authRequestOptions';

bootstrap(AppComponent, [HTTP_PROVIDERS, {provide: RequestOptions, useClass: AuthRequestOptions}, AdalService, ConfigService, AccountService]);