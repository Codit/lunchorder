import { Component, OnInit } from '@angular/core';
import { AdalService } from 'angular2-adal/core';
import { ConfigService } from './services/configService';
import { BadgeService } from './services/badgeService';
import { Badge } from './domain/dto/badge';
import { BadgeRow } from './app.badge-row';

@Component({
	selector: '[badges-list]',
	directives: [BadgeRow],
	 inputs: ['badges: badgesList'], 
	templateUrl: 'app.badges-list.html'})

export class BadgesList implements OnInit {

	constructor(private configService: ConfigService, private badgeService: BadgeService) { }
	badges: Badge[];
	badgeError: string;

	ngOnInit() {
		this.badgeService.getBadges().subscribe(
			badges => { 
				this.badges = badges;
				// todo map badges with current user badges
			},
			error => this.badgeError = <any>error);
	}
}