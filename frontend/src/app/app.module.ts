/// <reference path="../../node_modules/angular2-toaster/angular2-toaster.d.ts" />

import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule }   from '@angular/forms';

import { AppComponent } from './app.component';
import { AboutYouComponent } from './app.about-you';
import { AdminPrepayComponent } from './app.admin-prepay';
import { BadgeRow } from './app.badge-row';
import { BadgesList } from './app.badges-list';
import { FooterComponent } from './app.footer';
import { InformationComponent } from './app.information';
import { MenuComponent } from './app.menu';
import { MenuCategoryRow } from './app.menu-category-row';
import { MenuEntryRow } from './app.menu-entry-row';
import { ReminderComponent } from './app.reminder';

import { ConfigService } from './services/configService';
import { AccountService } from './services/accountService';
import { ErrorHandlerService } from './services/errorHandlerService';
import { MenuService } from './services/menuService';
import { BalanceService } from './services/balanceService';
import { BadgeService } from './services/badgeService';
import { OrderService } from './services/orderService';
import { ServiceworkerService } from './services/serviceworkerService';
import { HttpClient } from './helpers/httpClient';
import { TokenHelper } from './helpers/tokenHelper';
import { ReactiveFormsModule } from '@angular/forms';
import { ToasterModule, ToasterService } from 'angular2-toaster/angular2-toaster';
import { UPLOAD_DIRECTIVES } from 'ng2-uploader/ng2-uploader';

import { StickCartDirective } from './directives/stickCartDirective';

import { MenuEntryPipe } from './pipes/menuEntry.pipe';
import { MenuFilterPipe } from './pipes/menuFilter.pipe';

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        ToasterModule
    ],
    declarations: [
        AppComponent,
        AboutYouComponent,
        AdminPrepayComponent,
        BadgeRow,
        BadgesList,
        FooterComponent,
        InformationComponent,
        MenuComponent,
        MenuCategoryRow,
        MenuEntryRow,
        ReminderComponent,
        UPLOAD_DIRECTIVES,
        MenuEntryPipe,
        MenuFilterPipe
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
        StickCartDirective,
        {provide: Window, useValue: window}
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }