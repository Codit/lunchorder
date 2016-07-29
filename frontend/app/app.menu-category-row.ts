import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/configService';
import { MenuCategory } from './domain/dto/category';
import { MenuEntry } from './domain/dto/menuEntry';
import { MenuEntryRow } from './app.menu-entry-row';
import { MenuEntryPipe } from './pipes/menuEntry.pipe';
@Component({
	selector: '[menu-category-row]',
	directives: [MenuCategoryRow, MenuEntryRow],
	inputs: ['categoryItem: category', 'menuEntries'],
	template: `<div>
					<h4 *ngIf="categoryItem.subcategories">{{categoryItem.name}}</h4>
					<h5 *ngIf="!categoryItem.subcategories">{{categoryItem.name}}</h5>	

					<div menu-entry-row class="row" *ngFor="let menuEntry of menuEntries | menuEntryByCategoryId:categoryItem.id" [menuEntry]="menuEntry"></div>


					<div menu-category-row class="col-xs-12 col-md-6" *ngFor="let cat of categoryItem.subCategories" [category]="cat" [menuEntries]="menuEntries"></div>
				</div>`,
				pipes: [MenuEntryPipe]})
	
export class MenuCategoryRow implements OnInit {

	constructor(private configService: ConfigService) { }
	categoryItem : MenuCategory;
	menuEntries: MenuEntry[];

	ngOnInit() {
	}
}