import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/config.service';
import { MenuEntry } from './domain/dto/menuEntry';
import { MenuRule } from './domain/dto/menuRule';
import { OrderService } from './services/order.service';
import { MenuOrder } from './domain/dto/menuOrder';

@Component({
	selector: '[menu-entry-row]',
	inputs: ['menuEntry'],
	templateUrl: 'app.menu-entry-row.html'})

export class MenuEntryRow implements OnInit {

	constructor(private configService: ConfigService, private orderService: OrderService) { }
	menuEntry: MenuEntry;
	isModalOpen: boolean;

	ngOnInit() {
	}

	openModal() {
		this.isModalOpen = true;
	}

	addOrder(event : any, value : any) {

		this.closeModal();

		var menuOrder = new MenuOrder();
		menuOrder.menuEntryId = this.menuEntry.id;
		menuOrder.price = this.menuEntry.price;
		menuOrder.freeText = value.freeText;
		menuOrder.appliedMenuRules = new Array<MenuRule>();
		menuOrder.name = this.menuEntry.name;
		menuOrder.id = 1 + this.orderService.menuOrders.length;
		for (let menuRule of this.menuEntry.rules) {
		     if (value[menuRule.id]) {
			  menuOrder.appliedMenuRules.push(menuRule);
			  }
		}

		this.orderService.menuOrders.push(menuOrder);
	}

	closeModal() {
		this.isModalOpen = false;
	}
}