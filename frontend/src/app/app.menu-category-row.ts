import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/configService';
import { MenuCategory } from './domain/dto/category';
import { MenuEntry } from './domain/dto/menuEntry';

@Component({
	selector: '[menu-category-row]',
	inputs: ['categoryItem: category', 'menuEntries', 'menuFilterInputValue'],
	templateUrl: 'app.menu-category-row.html'
})

export class MenuCategoryRow implements OnInit {

	constructor(private configService: ConfigService) { }
	categoryItem: MenuCategory;
	menuEntries: MenuEntry[];

	ngOnInit() {
	}
}