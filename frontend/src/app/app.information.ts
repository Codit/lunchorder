import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/configService';
import { AccountService } from './services/accountService';
import { MenuService } from './services/menuService';
import { Menu } from './domain/dto/menu';

@Component({
	selector: '[information]',
	templateUrl: 'app.information.html' 
})

export class InformationComponent implements OnInit {

	constructor(private configService: ConfigService, private menuService: MenuService) { }

	menu: Menu;
	// todo, inspect error object.
	error: any;

	ngOnInit() {
		this.menuService.menu$.subscribe(
			(menu : Menu) => {
				this.menu = menu
			},
			error => this.error = <any>error);
	}
}