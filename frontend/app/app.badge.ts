import { Component, OnInit } from '@angular/core';
import { AdalService } from 'angular2-adal/core';
import { ConfigService } from './services/configService';

@Component({
	selector: '[badge]',
	template: `<div class="col-sm-6  block wow bounceIn">
					<div class="row">
						<div class="col-md-4 box-icon rotate">
							<i class="fa fa-desktop fa-4x "> </i>
						</div>
						<div class="col-md-8 box-ct">
							<h3> Bootstrap </h3>
							<p> Lorem ipsum dolor sit ametconsectetur adipiscing elit. Suspendisse orci quam. </p>
						</div>
					</div>
				</div>`})

export class BalanceComponent implements OnInit {

	constructor(private configService: ConfigService) { }

	ngOnInit() {
		
	}
}