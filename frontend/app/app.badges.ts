import { Component, OnInit } from '@angular/core';
import { AdalService } from 'angular2-adal/core';
import { ConfigService } from './services/configService';

@Component({
	selector: '[badges]',
	template: `<div class="container">
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
		</div>`})

export class BadgeComponent implements OnInit {

	constructor(private configService: ConfigService) { }

	ngOnInit() {
		
	}
}