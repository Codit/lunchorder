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
	template: `<div>
					<div *ngIf="categoryItem.subcategories">	
						<h4>{{categoryItem.name}}</h4>
					</div>
					<div *ngIf="!categoryItem.subcategories">
						<h5 *ngIf="!categoryItem.subcategories">{{categoryItem.name}}</h5>
					</div>	
					<p [hidden]="!categoryItem.description" class="cat-desc">{{categoryItem.description}}</p>

					<div menu-entry-row class="row" *ngFor="let menuEntry of menuEntries | menuEntryByCategoryId:categoryItem.id | menuFilter:menuFilterInputValue" [menuEntry]="menuEntry"></div>
					<div menu-category-row class="col-xs-12 col-md-6" *ngFor="let cat of categoryItem.subCategories" [category]="cat" [menuEntries]="menuEntries"></div>
				</div>`,
				pipes: [MenuEntryPipe, MenuFilterPipe]})
	
export class MenuCategoryRow implements OnInit {

	constructor(private configService: ConfigService) { }
	categoryItem : MenuCategory;
	menuEntries: MenuEntry[];

	ngOnInit() {
	}
}