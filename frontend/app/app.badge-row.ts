import { Component, OnInit } from '@angular/core';
import { AdalService } from 'angular2-adal/core';
import { ConfigService } from './services/configService';
import { Badge } from './domain/dto/badge';

@Component({
	selector: 'badge-row',
	inputs: ['badgeItem: badge'],
	template: `<div class="col-sm-6  block wow bounceIn">
					<div class="row">
						<div class="col-md-4 box-icon rotate">
							<i class="fa fa-desktop fa-4x "> </i>
						</div>
						<div class="col-md-8 box-ct">
							<h3>{{badgeItem?.name}}</h3>
							<!--<p> Lorem ipsum dolor sit ametconsectetur adipiscing elit. Suspendisse orci quam. </p>-->
						</div>
					</div>
				</div>`})

export class BadgeRow implements OnInit {

	constructor(private configService: ConfigService) { }
	badgeItem : Badge;

	ngOnInit() {
		console.log(`badge: ${this.badgeItem.name}`)
	}
}