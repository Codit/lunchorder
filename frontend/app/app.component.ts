import { Component, OnInit } from '@angular/core';
import { ROUTER_DIRECTIVES, RouteConfig, RouterOutlet } from '@angular/router-deprecated';
import { AdalService } from 'angular2-adal/core';
import { ConfigService } from './services/configService';
import { AccountService } from './services/accountService';

@Component({
	selector: 'my-app',
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

	<!-- Login -->
	<div id="login" class="content-section-b" style="border-top: 0">
		<div class="container">

			<!--
			<div class="col-md-6 col-md-offset-3 text-center wrap_title">
				<h2>Login</h2>
				<p class="lead" style="margin-top:0">Please login to continue</p>
			</div>
			
			<div class="row">
			
				<div class="col-sm-4 wow fadeInDown text-center">
				  <img class="rotate" src="css/images/icon/tweet.svg" alt="Generic placeholder image">
				  <h3>About this project</h3>
				  <p class="lead">This project </p>

				  
				</div>
				
				<div class="col-sm-4 wow fadeInDown text-center">
				  <img  class="rotate" src="css/images/icon/picture.svg" alt="Generic placeholder image">
				   <h3>Gallery</h3>
				   <p class="lead">Epsum factorial non deposit quid pro quo hic escorol. Olypian quarrels et gorilla congolium sic ad nauseum. </p>
				   
				</div>
				
				<div class="col-sm-4 wow fadeInDown text-center">
				  <img  class="rotate" src="css/images/icon/retina.svg" alt="Generic placeholder image">
				   <h3>Retina</h3>
					<p class="lead">Epsum factorial non deposit quid pro quo hic escorol. Olypian quarrels et gorilla congolium sic ad nauseum. </p>
				  
				</div>
				
			</div>
			-->

			<div class="row tworow">

				<div class="col-sm-4  wow fadeInDown text-center">
					<img class="rotate" src="css/images/icon/watch.svg" alt="Generic placeholder image">
					<h3>On Time</h3>
					<p class="lead">Orders are sent to the venue @ 9:30 am. Late orders are your own responsibility and should be made by giving them a
						call. </p>
					<!-- <p><a class="btn btn-embossed btn-primary view" role="button">View Details</a></p> -->
				</div>
				<!-- /.col-lg-4 -->

				<div class="col-sm-4 wow fadeInDown text-center">
					<img class="rotate" src="css/images/icon/map.svg" alt="Generic placeholder image">
					<h3>Venue</h3>
					<p class="lead">Venue placeholder details come here </p>
					<!-- <p><a class="btn btn-embossed btn-primary view" role="button">View Details</a></p> -->
				</div>
				<!-- /.col-lg-4 -->

				<div class="col-sm-4 wow fadeInDown text-center">
					<img class="rotate" src="css/images/icon/savings.svg" alt="Generic placeholder image">
					<h3>Payment</h3>
					<p class="lead">Payment is done upfront. Please consult your user profile to add funds to your account. </p>
					<!-- <p><a class="btn btn-embossed btn-primary view" role="button">View Details</a></p> -->
				</div>
				<!-- /.col-lg-4 -->

			</div>
			<!-- /.row -->
		</div>
	</div>

	<!-- Order -->
	<div id="order" class="content-section-a">

		<div class="container">

			<div class="row">

				<div class="col-sm-6 pull-right wow fadeInRightBig">
					<img class="img-responsive " src="css/images/ipad.png" alt="">
				</div>

				<div class="col-sm-6 wow fadeInLeftBig" data-animation-delay="200">
					<h3 class="section-heading">Lunch Menu</h3>
					<div class="sub-title lead3">Lorem ipsum dolor sit atmet sit dolor greand fdanrh<br> sdfs sit atmet sit dolor greand fdanrh sdfs</div>
					<p class="lead">
						In his igitur partibus duabus nihil erat, quod Zeno commuta rest gestiret. Sed virtutem ipsam inchoavit, nihil ampliusuma.
						Scien tiam pollicentur, uam non erat mirum sapientiae lorem cupido patria esse cariorem. Quae qui non vident, nihilamane
						umquam magnum ac cognitione.
					</p>

					<p><a class="btn btn-embossed btn-primary" href="#" role="button">View Details</a>
						<a class="btn btn-embossed btn-info" href="#" role="button">Visit Website</a></p>
				</div>
			</div>
		</div>
		<!-- /.container -->
	</div>


	<!-- Balance -->
	<div id="balance" class="content-section-b">
		<div class="container">
			<div class="row">
				<div class="col-md-6 col-md-offset-3 text-center wrap_title ">
					<h2>Balance</h2>
					<p class="lead" style="margin-top:0">Check your balance and other options.</p>
				</div>
			</div>
		</div>
	</div>

	<div class="content-section-c ">
		<div class="container">
			<div class="row">
				<div class="col-md-6 col-md-offset-3 text-center white">
					<h2>Get Reminders</h2>
					<p class="lead" style="margin-top:0">Never miss an order.</p>
				</div>
				<div class="col-md-6 col-md-offset-3 text-center">
					<div class="mockup-content">
						<div class="morph-button wow pulse morph-button-inflow morph-button-inflow-1">
							<button type="button "><span>Subscribe to our Newsletter</span></button>
							<div class="morph-content">
								<div>
									<div class="content-style-form content-style-form-4 ">
										<h2 class="morph-clone">Subscribe to our Newsletter</h2>
										<form>
											<p><label>Your Email Address</label><input type="text" /></p>
											<p><button>Remind me</button></p>
										</form>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>>
		</div>
	</div>

	<!-- Badges -->
	<div id="badges" class="content-section-a">
		<div class="container">
			<div class="row">

				<div class="col-md-6 col-md-offset-3 text-center wrap_title">
					<h2>Badges</h2>
					<p class="lead" style="margin-top:0">Earn badges along the way.</p>
				</div>

				<div class="col-sm-6  block wow bounceIn">
					<div class="row">
						<div class="col-md-4 box-icon rotate">
							<i class="fa fa-desktop fa-4x "> </i>
						</div>
						<div class="col-md-8 box-ct">
							<h3> Bootstrap </h3>
							<p> Lorem ipsum dolor sit ametconsectetur adipiscing elit. Suspendisse orci quam. </p>
						</div>
					</div>
				</div>
				<div class="col-sm-6 block wow bounceIn">
					<div class="row">
						<div class="col-md-4 box-icon rotate">
							<i class="fa fa-picture-o fa-4x "> </i>
						</div>
						<div class="col-md-8 box-ct">
							<h3> Owl-Carousel </h3>
							<p> Nullam mo arcu ac molestie scelerisqu vulputate, molestie ligula gravida, tempus ipsum.</p>
						</div>
					</div>
				</div>
			</div>

			<div class="row tworow">
				<div class="col-sm-6  block wow bounceIn">
					<div class="row">
						<div class="col-md-4 box-icon rotate">
							<i class="fa fa-magic fa-4x "> </i>
						</div>
						<div class="col-md-8 box-ct">
							<h3> Codrops </h3>
							<p> Lorem ipsum dolor sit ametconsectetur adipiscing elit. Suspendisse orci quam. </p>
						</div>
					</div>
				</div>
				<div class="col-sm-6 block wow bounceIn">
					<div class="row">
						<div class="col-md-4 box-icon rotate">
							<i class="fa fa-heart fa-4x "> </i>
						</div>
						<div class="col-md-8 box-ct">
							<h3> Lorem Ipsum</h3>
							<p> Nullam mo arcu ac molestie scelerisqu vulputate, molestie ligula gravida, tempus ipsum.</p>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>

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
	userInfo : api.dto.IGetUserInfoResponse;
	userInfoError: any;

	constructor(private adalService: AdalService, private configService: ConfigService, private accountService: AccountService) { }

	ngOnInit() {
		debugger;
		this.accountService.getUserProfile().subscribe(
                       userInfo => this.userInfo = userInfo,
                       error =>  this.userInfoError = <any>error);

		this.adalService.init(this.configService.adalConfig);
		console.log('ctor AppComponent');
        this.adalService.handleWindowCallback();
		if (this.adalService) {
			this.isAuthenticated = this.adalService.userInfo.isAuthenticated
			console.log(`isAuthenticated: ${this.isAuthenticated}`);
		}
	}

	public login() {
        this.adalService.login();
    }
}