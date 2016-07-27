import { Component, OnInit } from '@angular/core';
import { AdalService } from 'angular2-adal/core';
import { ConfigService } from './services/configService';
import { Badge } from './domain/dto/badge';

@Component({
	selector: 'badge-row',
	inputs: ['badgeItem: badge'],
	template: `<div class="block wow bounceIn">
						<div class="col-xs-6 col-sm-4 col-md-3 col-lg-2">
							<img src='/css/images/badges/{{badgeItem?.icon}}' title='{{badgeItem?.description}}' style='opacity: 0.3;
								max-height: 100%;
								max-width: 100%;' />
	<p class='text-center'>{{badgeItem?.name}}</p>
						</div>
				</div>`})

export class BadgeRow implements OnInit {

	constructor(private configService: ConfigService) { }
	badgeItem : Badge;

	ngOnInit() {
		console.log(`badge: ${this.badgeItem.name}`)
	}
}