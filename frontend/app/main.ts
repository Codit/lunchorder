/// <reference path="../node_modules/angular2-toaster/angular2-toaster.d.ts" />

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
import { BadgeService } from './services/badgeService';
import { OrderService } from './services/orderService';
import {enableProdMode} from '@angular/core';
import { HttpClient } from './helpers/httpClient';
import { TokenHelper } from './helpers/tokenHelper';
import { WindowRef, BrowserWindowRef, WINDOW } from './services/windowService';
import { ToasterService } from 'angular2-toaster/angular2-toaster';

if (window.location.href.indexOf('localhost') < 0) {
    enableProdMode();
}
bootstrap(AppComponent, [HTTP_PROVIDERS, 
  disableDeprecatedForms(),     // Disable old Forms API!
  provideForms(),                // Use new Forms API!
{provide: RequestOptions, useClass: AuthRequestOptions}, AdalService, ConfigService, AccountService, MenuService, BalanceService, ToasterService, BadgeService, OrderService, HttpClient, TokenHelper, 
{ provide: WindowRef, useClass: BrowserWindowRef }, { provide: WINDOW, useFactory: _window, deps: [] }

])
.catch((err: any) => console.error(err));

function _window(): any {
    return window;
}