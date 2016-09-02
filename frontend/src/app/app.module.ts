/// <reference path="../../node_modules/angular2-toaster/angular2-toaster.d.ts" />

import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule }   from '@angular/forms';

import { bootstrap }    from '@angular/platform-browser-dynamic';
import { AppComponent } from './app.component';
import { ConfigService } from './services/configService';
import { AccountService } from './services/accountService';
import { ErrorHandlerService } from './services/errorHandlerService';
import { MenuService } from './services/menuService';
import { BalanceService } from './services/balanceService';
import { BadgeService } from './services/badgeService';
import { OrderService } from './services/orderService';
import { HttpClient } from './helpers/httpClient';
import { TokenHelper } from './helpers/tokenHelper';
import { WindowRef, BrowserWindowRef, WINDOW } from './services/windowService';
import { ReactiveFormsModule } from '@angular/forms';
import { ToasterModule, ToasterService } from 'angular2-toaster/angular2-toaster';

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        ToasterModule
    ],
    declarations: [
        AppComponent
    ],
    providers: [
        AccountService,
        BalanceService,
        BadgeService,
        ConfigService,
        ErrorHandlerService,
        HttpClient,
        MenuService,
        OrderService,
        ToasterService,
        TokenHelper,
        { provide: WindowRef, useClass: BrowserWindowRef },
        { provide: WINDOW, useFactory: _window, deps: [] }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }

function _window(): any {
    return window;
}