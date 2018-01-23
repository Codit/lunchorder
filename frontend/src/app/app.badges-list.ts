import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/config.service';
import { BadgeService } from './services/badge.service';
import { Badge } from './domain/dto/badge';
import { BadgeRow } from './app.badge-row';
import { AccountService } from './services/account.service';

@Component({
	selector: '[badges-list]',
	inputs: ['badges: badgesList'],
	templateUrl: 'app.badges-list.html'
})

export class BadgesList implements OnInit {

	constructor(private configService: ConfigService, private badgeService: BadgeService, private accountService: AccountService) { }
	badges: Badge[];
	badgeError: string;

	ngOnInit() {
		this.badgeService.getBadges().subscribe(
			badges => {
				this.badges = badges;

				this.accountService.user$.subscribe(user => {
					if (user.badges) {
						user.badges.forEach(badge => {
								var pBadge = this.badges.find(pBadge => {
									return pBadge.id == badge.badgeId;
								});
								if (pBadge) {
									pBadge.timesEarned = badge.timesEarned;
								}
						})
					}
				  });
			},
			error => this.badgeError = <any>error);
	}
}