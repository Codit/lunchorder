import { Component, OnInit } from '@angular/core';
import { AdalService } from 'angular2-adal/core';
import { ConfigService } from './services/configService';
import { Badge } from './domain/dto/badge';
import { BadgeRow } from './app.badge-row';

@Component({
	selector: '[badges-list]',
	directives: [BadgeRow],
	 inputs: ['badges: badgesList'], 
	template: `
	<div class="container">
		<badge-row *ngFor="let b of badges" [badge]="b"></badge-row> 
	</div>`})

export class BadgesList implements OnInit {

	constructor(private configService: ConfigService) { }
	badges: Badge[];

	ngOnInit() {
		
	}
}