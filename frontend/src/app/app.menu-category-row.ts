import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/configService';
import { MenuCategory } from './domain/dto/category';
import { MenuEntry } from './domain/dto/menuEntry';
import { MenuEntryRow } from './app.menu-entry-row';
import { MenuEntryPipe } from './pipes/menuEntry.pipe';
import { MenuFilterPipe } from './pipes/menuFilter.pipe';

@Component({
	selector: '[menu-category-row]',
	directives: [MenuCategoryRow, MenuEntryRow],
	inputs: ['categoryItem: category', 'menuEntries', 'menuFilterInputValue'],
	templateUrl: 'app.menu-category-row.html',
				pipes: [MenuEntryPipe, MenuFilterPipe]})
	
export class MenuCategoryRow implements OnInit {

	constructor(private configService: ConfigService) { }
	categoryItem : MenuCategory;
	menuEntries: MenuEntry[];

	ngOnInit() {
	}
}