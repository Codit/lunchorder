import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/configService';
import { AccountService } from './services/accountService';
import { BalanceService } from './services/balanceService';
import { InformationComponent } from './app.information';
import { MenuComponent } from './app.menu';
import { AboutYouComponent } from './app.about-you';
import { ReminderComponent } from './app.reminder';
import { BadgesList } from './app.badges-list';
import { Badge } from './domain/dto/badge';
import { ErrorDescriptor } from './domain/dto/errorDescriptor';
import { LoginForm } from './domain/dto/loginForm';
import { AdminPrepayComponent } from './app.admin-prepay';
import { FooterComponent } from './app.footer';
import { StickRxDirective } from './directives/stickDirective';
import { ToasterConfig, ToasterService } from 'angular2-toaster/angular2-toaster';
import { ServiceworkerService } from './services/serviceworkerService';

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

	constructor(private accountService: AccountService, private configService: ConfigService, private toasterService: ToasterService, private serviceworkerService: ServiceworkerService) { 
		serviceworkerService.init();
	}

	ngOnInit() {
		this.loginForm = new LoginForm();

		this.accountService.isAuthenticated$.subscribe((value) => {
			this.isAuthenticated = value;
			debugger;
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