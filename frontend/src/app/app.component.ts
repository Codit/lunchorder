import { Component, OnInit } from '@angular/core';
import { ROUTER_DIRECTIVES, RouteConfig, RouterOutlet} from '@angular/router-deprecated';
import { ConfigService } from './services/configService';
import { AccountService } from './services/accountService';
import { BalanceService } from './services/balanceService';
import { InformationComponent } from './app.information';
import { MenuComponent } from './app.menu';
import { BalanceComponent } from './app.balance';
import { ReminderComponent } from './app.reminder';
import { BadgesList } from './app.badges-list';
import { Badge } from './domain/dto/badge';
import { ErrorDescriptor } from './domain/dto/errorDescriptor';
import { LoginForm } from './domain/dto/loginForm';
import { AdminPrepayComponent } from './app.admin-prepay';
import { StickRxDirective } from './directives/stickDirective';
import {ToasterContainerComponent, ToasterService, ToasterConfig} from 'angular2-toaster/angular2-toaster';

@Component({
	selector: 'lunchorder-app',
	// question: why do we need a provider here for a component that has its own descriptor?
	providers: [BalanceService, ToasterService],
	directives: [InformationComponent, MenuComponent, BalanceComponent, ReminderComponent, BadgesList, StickRxDirective, AdminPrepayComponent, ToasterContainerComponent],

	templateUrl: 'app.component.html'})

export class AppComponent implements OnInit {
	userInfo: app.domain.dto.IGetUserInfoResponse;
	userInfoError: any;
	userBadges: Badge[] = new Array<Badge>();;
	public toasterconfig: ToasterConfig = new ToasterConfig({
		positionClass: 'toast-bottom-right'
	});
	loginForm: LoginForm;

	constructor(private accountService: AccountService, private configService: ConfigService, private toasterService: ToasterService) { }

	ngOnInit() {
		this.loginForm = new LoginForm();

		this.accountService.isAuthenticated$.subscribe((value) => {
			this.isAuthenticated = value;
			debugger;
		})
	}

	isAuthenticated : boolean = false;

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

	public isAdminPrepay() {
		if (this.accountService.user && this.accountService.user.roles) {
			return this.accountService.user.roles.find(x => x == "prepay-admin");
		}
		return null;
	}

	public login() {
        this.accountService.login();
    }
}