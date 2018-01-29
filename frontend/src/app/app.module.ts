import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

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

import { WindowRef } from './services/windowRef';
import { ConfigService } from './services/config.service';
import { AccountService } from './services/account.service';
import { ErrorHandlerService } from './services/error-handler.service';
import { MenuService } from './services/menu.service';
import { BalanceService } from './services/balance.service';
import { BadgeService } from './services/badge.service';
import { OrderService } from './services/order.service';
import { HttpClient } from './helpers/httpClient';
import { TokenHelper } from './helpers/tokenHelper';
import { ReactiveFormsModule } from '@angular/forms';
import { ToasterModule, ToasterService } from 'angular2-toaster/angular2-toaster';
import { NgUploaderModule } from 'ngx-uploader';

import { StickCartDirective } from './directives/stickCartDirective';

import { MenuEntryPipe } from './pipes/menu-entry.pipe';
import { MenuFilterPipe } from './pipes/menu-filter.pipe';
import { LightboxModule } from 'angular2-lightbox';

@NgModule({
    imports: [
        BrowserAnimationsModule,
        BrowserModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        ToasterModule,
        NgUploaderModule,
        LightboxModule 
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
        MenuEntryPipe,
        MenuFilterPipe,
        StickCartDirective
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
        WindowRef
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }