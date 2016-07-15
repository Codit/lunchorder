/// <reference path="../node_modules/automapper-ts/dist/automapper-classes.d.ts" />
/// <reference path="../node_modules/automapper-ts/dist/automapper-interfaces.d.ts" />
/// <reference path="../node_modules/automapper-ts/dist/automapper-declaration.d.ts" />

import 'zone';
import 'reflect-metadata';
import { bootstrap }    from '@angular/platform-browser-dynamic';
import { disableDeprecatedForms, provideForms } from '@angular/forms'
import { AdalService } from 'angular2-adal/core';
import { HTTP_PROVIDERS, BaseRequestOptions, RequestOptions } from '@angular/http';
import { AppComponent } from './app.component';
import { ConfigService } from './services/configService';
import { AccountService } from './services/accountService';
import { AuthRequestOptions } from './helpers/authRequestOptions';
import { MenuService } from './services/menuService';
import { BalanceService } from './services/balanceService';
import {enableProdMode} from '@angular/core';
import { HttpClient } from './helpers/httpClient';

if (window.location.href.indexOf('localhost') < 0) {
    enableProdMode();
}
bootstrap(AppComponent, [HTTP_PROVIDERS, 
  disableDeprecatedForms(),     // Disable old Forms API!
  provideForms(),                // Use new Forms API!
{provide: RequestOptions, useClass: AuthRequestOptions}, AdalService, ConfigService, AccountService, MenuService, BalanceService, HttpClient])
.catch((err: any) => console.error(err));