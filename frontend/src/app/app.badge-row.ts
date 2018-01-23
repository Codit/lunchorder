import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/config.service';
import { Badge } from './domain/dto/badge';

@Component({
	selector: 'badge-row',
	inputs: ['badgeItem: badge'],
	templateUrl: 'app.badge-row.html'})

export class BadgeRow implements OnInit {

	constructor(private configService: ConfigService) { }
	badgeItem : Badge;
	hasBadge: boolean;

	ngOnInit() {
		if (this.badgeItem.timesEarned > 0) {
			this.hasBadge = true;
		}
	}
}