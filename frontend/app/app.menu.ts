import { Component, OnInit } from '@angular/core';
import { AdalService } from 'angular2-adal/core';
import { ConfigService } from './services/configService';
import { AccountService } from './services/accountService';
import { MenuService } from './services/menuService';
import { Menu } from './domain/dto/menu';
import { MenuCategoryRow } from './app.menu-category-row';
import { MenuEntry } from './domain/dto/menuEntry';

@Component({
	selector: '[menu]',
	directives: [MenuCategoryRow],
	template: `<div class="container">
			<div class="row">
				<div class="col-xs-12 wow fadeInLeftBig" data-animation-delay="200">
					<h3 class="section-heading">Lunch Menu</h3>
					<div menu-category-row class="col-xs-12" *ngFor="let cat of menu?.categories" [category]="cat" [menuEntries]="menu?.entries"></div>
				</div>
			</div>
		</div>`,
})

export class MenuComponent implements OnInit {

	constructor(private configService: ConfigService, private menuService: MenuService) { }

	menu: Menu;
	// todo, inspect error object.
	error: any;

	ngOnInit() {
		this.menuService.getMenu().subscribe(
			menu => {
				this.menu = menu
			},
			error => this.error = <any>error);
	}
}