import { Component, OnInit } from '@angular/core';
import { ROUTER_DIRECTIVES, RouteConfig, RouterOutlet} from '@angular/router-deprecated';
import { AdalService } from 'angular2-adal/core';
import { ConfigService } from './services/configService';
import { AccountService } from './services/accountService';
import { BalanceService } from './services/balanceService';
import { InformationComponent } from './app.information';
import { MenuComponent } from './app.menu';
import { BalanceComponent } from './app.balance';
import { ReminderComponent } from './app.reminder';
import { BadgeComponent } from './app.badges';
import { TokenHelper } from './helpers/tokenHelper';

@Component({
	selector: 'lunchorder-app',
	// question: why do we need a provider here for a component that has its own descriptor?
	providers: [BalanceService],
	directives: [InformationComponent, MenuComponent, BalanceComponent, ReminderComponent, BadgeComponent],
	template: `<!-- FullScreen -->
	<div class="intro-header">
		<div class="col-xs-12 text-center abcen1">
			<h1 class="h1_home wow fadeIn" data-wow-delay="0.4s">Lunch Order</h1>
			<h3 class="h3_home wow fadeIn" data-wow-delay="0.6s">Order lunch at your enterprise with ease</h3>
			<ul class="list-inline intro-social-buttons">
				<li><button class="btn btn-lg mybutton_cyano wow fadeIn" data-wow-delay="0.8s" (click)="login()"><span class="network-name">Login using Company Account</span></button>
				</li>
			</ul>
		</div>
		<!-- /.container -->
		<div class="col-xs-12 text-center abcen wow fadeIn">
			<div class="button_down ">
				<a class="imgcircle wow bounceInUp" data-wow-duration="1.5s" href="#order"> <img class="img_scroll" src="css/images/icon/circle.png" alt=""> </a>
			</div>
		</div>
	</div>
  
<div [hidden]="!isAuthenticated">
	<!-- NavBar-->
	<nav class="navbar-default" role="navigation">
		<div class="container">
			<div class="navbar-header">
				<button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-ex1-collapse">
					<span class="sr-only">Toggle navigation</span>
					<span class="icon-bar"></span>
					<span class="icon-bar"></span>
					<span class="icon-bar"></span>
				</button>
				<a class="navbar-brand" href="#home">LunchOrder</a>
			</div>

			<div class="collapse navbar-collapse navbar-right navbar-ex1-collapse">
				<ul class="nav navbar-nav">

					<li class="menuItem"><a href="#order">Order</a></li>
					<li class="menuItem"><a href="#balance">Balance</a></li>
					<li class="menuItem"><a href="#badges">Badges</a></li>
					<li class="menuItem"><a href="#contact">Contact</a></li>
				</ul>
			</div>

		</div>
	</nav>

	<div information id="information" class="content-section-b" style="border-top: 0"></div>

	<div menu id="order" class="content-section-a"></div>


	<div balance id="balance" class="content-section-b"></div>

	<div reminder  class="content-section-c "></div>

	<div badges id="badges" class="content-section-a"></div>

	<!-- Banner Download -->
	<div id="downloadlink" class="banner">
		<div class="container">
			<div class="row">
				<div class="col-md-8 col-md-offset-4 text-right wrap_title">
					<h2>Enjoy your lunch</h2>
					<p class="lead" style="margin-top:0">Tweet your order</p>
				</div>
			</div>
		</div>
	</div>
</div>

	<footer>
		<div class="container">
			<div class="row">
				<div class="col-md-7">
					<h3 class="footer-title">About</h3>
					<p>This project started as an R&amp;D project for <a href="https://angular.io/" target="_blank">Angular2</a>. It has been open sourced by <a href="http://www.codit.eu/" target="_blank">Codit</a> on github so feel free to contribute or request features.</p>
					<p>Lunchorder is licensed under The MIT License (MIT). Which means that you can use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the web application. But you always need to state that Codit is the original author of this web application.</p>

					<p><a class="imgcircle wow bounceInUp" data-wow-duration="1.5s" href="http://www.codit.eu"> <img class="pull-right" src="css/images/logo/logo_codit_100px.png" alt="Codit.eu"> </a></p>
				</div>
				<!-- /col-xs-7 -->

				<div class="col-md-5">
					<div class="footer-banner">
						<h3 class="footer-title">Lunch Order</h3>
						<ul>
							<li>sends out orders on time</li>
							<li>users pay in advance</li>
							<li>lets you order from anywhere</li>
							<li>reminds you in case you forget</li>
							<li>hands out fancy badges</li>
						</ul>
						<iframe src="https://ghbtns.com/github-btn.html?user=coditeu&repo=lunchorder&type=star&count=true" frameborder="0" scrolling="0" width="170px" height="20px"></iframe>
					</div>
				</div>
			</div>
		</div>
		</footer>`})

export class AppComponent implements OnInit {
	isAuthenticated: boolean = false;
	userInfo : app.domain.dto.IGetUserInfoResponse;
	userInfoError: any;

	constructor(private adalService: AdalService, private configService: ConfigService, private accountService: AccountService, private tokenHelper: TokenHelper) { }

	ngOnInit() {
		this.accountService.getUserProfile().subscribe(
                       userInfo => this.userInfo = userInfo,
                       error =>  this.userInfoError = <any>error);

		this.adalService.init(this.configService.adalConfig);
		console.log('ctor AppComponent');
        this.adalService.handleWindowCallback();
		if (this.adalService) {
			this.isAuthenticated = this.adalService.userInfo.isAuthenticated
			console.log(`isAuthenticated: ${this.isAuthenticated}`);

			this.tokenHelper.getToken();
		}
		
	}

	public login() {
        this.adalService.login();
    }
}