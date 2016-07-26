import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/configService';
import { MenuEntry } from './domain/dto/menuEntry';

@Component({
	selector: '[menu-entry-row]',
	inputs: ['menuEntry'],
	template: `<div class="col-xs-offset-2 col-md-offset-1"><i class="fa fa-shopping-basket" title="add to shopping basket"></i>{{menuEntry.name}}</div>`})
	 
export class MenuEntryRow implements OnInit {

	constructor(private configService: ConfigService) { }
	menuEntry: MenuEntry;

	ngOnInit() {
		debugger;
		console.log(`-- menu entryyyy: ${this.menuEntry.name}`)
	}
}