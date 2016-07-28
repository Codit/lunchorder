import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/configService';
import { MenuEntry } from './domain/dto/menuEntry';

@Component({
	selector: '[menu-entry-row]',
	inputs: ['menuEntry'],
	template: `<div class="col-xs-offset-2 col-md-offset-1">
					<i class="fa fa-shopping-basket" title="add to shopping basket"></i>
					<span>{{menuEntry.name}}</span>
					<button class="btn btn-primary btn-xs pull-right" style="margin-right: 20px; font-weight:bold;">&euro; {{menuEntry.price}}
					 <span class="btn-separator"></span>
					 <i class="fa fa-plus"></i></button>
			   </div>`})
	 
export class MenuEntryRow implements OnInit {

	constructor(private configService: ConfigService) { }
	menuEntry: MenuEntry;

	ngOnInit() {
		debugger;
		console.log(`-- menu entryyyy: ${this.menuEntry.name}`)
	}
}