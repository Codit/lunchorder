import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/config.service';
import { AccountService } from './services/account.service';
import { MenuService } from './services/menu.service';
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