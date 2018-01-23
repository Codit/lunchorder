import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/config.service';
import { AccountService } from './services/account.service';
import { BalanceService } from './services/balance.service';
import { InformationComponent } from './app.information';
import { MenuComponent } from './app.menu';
import { AboutYouComponent } from './app.about-you';
import { BadgesList } from './app.badges-list';
import { Badge } from './domain/dto/badge';
import { ErrorDescriptor } from './domain/dto/errorDescriptor';
import { LoginForm } from './domain/dto/loginForm';
import { AdminPrepayComponent } from './app.admin-prepay';
import { FooterComponent } from './app.footer';
import { StickRxDirective } from './directives/stickDirective';
import { ToasterConfig, ToasterService } from 'angular2-toaster/angular2-toaster';
import { GetUserInfoResponse } from './domain/dto/getUserInfoResponse'
import * as moment from 'moment';

@Component({
	selector: 'lunchorder-app',
	templateUrl: 'app.component.html'
})

export class AppComponent implements OnInit {
	userInfo: app.domain.dto.IGetUserInfoResponse;
	userInfoError: any;
	userBadges: Badge[] = new Array<Badge>();;
	public toasterconfig: ToasterConfig = new ToasterConfig({
		positionClass: 'toast-bottom-right'
	});
	loginForm: LoginForm;

	constructor(private accountService: AccountService, private configService: ConfigService, private toasterService: ToasterService) {
		this.accountService.user$.subscribe(user => {
			this.user = user;
		});
	}

	getIntroClass() {
		if (!this._introClass) {
			if (this.getEaster()) {
				return this._introClass = 'intro-header-img6';
			}

			var quarter = moment().quarter();
			return this._introClass = `intro-header-img${quarter}`;
		}
		else {
			return this._introClass;
		}

	}
	private _introClass: string;
	getEaster(): boolean {
		var easterDates = ['2017/04/16', '2018/04/01', '2019/04/21', '2020/04/12', '2021/04/04'];
		for (var easterDate of easterDates) {
			var today = moment();
			var startEaster = moment(easterDate);
			var endEaster = moment(easterDate).add(3, 'd');

			if (today.isBetween(startEaster, endEaster, 'days', '[]')) {
				return true;
			}
		}
		return false;
	}

	user: GetUserInfoResponse;

	ngOnInit() {
		this.loginForm = new LoginForm();

		this.accountService.isAuthenticated$.subscribe((value) => {
			this.isAuthenticated = value;
		});
	}

	isAuthenticated: boolean = false;

	loginUserPass($event: any, form: any) {
		this.accountService.loginUserPassword(this.loginForm).subscribe(
			response => {
				console.log(response);
			},
			error => {
				var errorDescriptor = error as ErrorDescriptor;
				this.toasterService.pop('error', errorDescriptor.title, errorDescriptor.description);
			}
		);

	}

	public isLoggedIn() {
		if (this.user) {
			return true;
		}
		return false;
	}

	public isAdminPrepay() {
		if (this.user && this.user.roles) {
			return this.user.roles.find(x => x == "prepay-admin");
		}
		return null;
	}

	public login() {
		this.accountService.login();
	}
}