import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/config.service';
import { BadgeService } from './services/badge.service';
import { Badge } from './domain/dto/badge';
import { BadgeRow } from './app.badge-row';
import { AccountService } from './services/account.service';
import { GetUserInfoResponse } from './domain/dto/getUserInfoResponse';
import { Lightbox, LightboxEvent, LightboxConfig } from 'angular2-lightbox';
import { BadgeRanking } from './domain/dto/badgeRanking';

@Component({
	selector: '[badges-list]',
	inputs: ['badges: badgesList'],
	templateUrl: 'app.badges-list.html'
})

export class BadgesList implements OnInit {

	constructor(private configService: ConfigService, private badgeService: BadgeService, private accountService: AccountService, private _lightbox: Lightbox,
		private _lightboxEvent: LightboxEvent,
		private _lighboxConfig: LightboxConfig) { }
	badges: Badge[];
	badgeRankings: BadgeRanking[];
	badgeError: string;
	user: GetUserInfoResponse;
	private _albums: Array<any> = [];

	ngOnInit() {
		this.badgeService.getBadges().subscribe(
			response => {
				this.badges = response.badges;
				this.badgeRankings = response.badgeRankings;
				
				this.badges.forEach(badge => {
					const src = '/assets/' + badge.image;
					const album = {
						src: src,
						caption: badge.description
					}
					this._albums.push(album);
				});
				
				this.accountService.user$.subscribe(user => {
					this.user = user;
					if (user.badges) {
						user.badges.forEach(badge => {
								debugger;
								var pBadge = this.badges.find(pBadge => {
									return pBadge.id == badge.id;
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

	open(index: number): void {
		this._lightbox.open(this._albums, index, { wrapAround: true});
	  }
}