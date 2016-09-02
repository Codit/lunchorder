import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/configService';
import { AccountService } from './services/accountService';
import { MenuService } from './services/menuService';
import { Menu } from './domain/dto/menu';
import { MenuCategoryRow } from './app.menu-category-row';
import { MenuEntry } from './domain/dto/menuEntry';
import { MenuOrder } from './domain/dto/menuOrder';
import { StickCartDirective } from './directives/stickCartDirective';
import { OrderService } from './services/orderService';
import { ToasterService } from 'angular2-toaster/angular2-toaster';

@Component({
	selector: '[menu]',
	directives: [MenuCategoryRow, StickCartDirective],
	templateUrl: 'app.menu.html'
})

export class MenuComponent implements OnInit {

	// todo move orderservice button to other component.
	constructor(private configService: ConfigService, private menuService: MenuService, private orderService: OrderService, private accountService: AccountService, private toasterService: ToasterService) { }
	isModalOpen: boolean;
	menu: Menu;
	// todo, inspect error object.
	error: any;
	isBusy: boolean;
	isBusyMenu: boolean = true;
	isClosed: boolean = false;

	ngOnInit() {
		var dayOfWeek = new Date().getDay();

		// todo, always open for demo
		this.isClosed = (dayOfWeek == 6) || (dayOfWeek == 0);

		if(this.isClosed) { this.isBusyMenu = false; }

		if (!this.isClosed) {
			this.menuService.getMenu().subscribe(
				menu => {
					if (menu.vendor.isClosingDate()) {
						this.isClosed = true;
					}
					else {
						this.menu = menu
					}
					this.isBusyMenu = false;
				},
				error => this.error = <any>error);
		}
	}

	getBalance(): number {
		return this.accountService.user.balance - this.orderService.totalPrice();
	}

	removeOrders(menuOrder: MenuOrder) {
		this.orderService.menuOrders = new Array<MenuOrder>();
	}

	removeOrder(menuOrder: MenuOrder) {
		var matchIndex: number = -1;
		for (var i = 0; i < this.orderService.menuOrders.length; i++) {
			if (menuOrder.id === this.orderService.menuOrders[i].id) {
				matchIndex = i;
				break;
			}
		}

		if (matchIndex >= 0) {
			this.orderService.menuOrders.splice(matchIndex, 1);
		}
	}

	openCheckout() {
		this.isModalOpen = true;
	}

	closeModal() {
		this.isModalOpen = false;
	}

	finalizeOrder() {
		this.isBusy = true;

		this.orderService.postMenuOrders().subscribe(menu => {
			this.toasterService.pop('success', 'Success', 'Order submitted');
			console.log("current balance: " + this.accountService.user.balance);
			console.log("total price: " + this.orderService.totalPrice())
			this.accountService.user.balance -= this.orderService.totalPrice();
			console.log("differnt balance: " + this.accountService.user.balance);
			this.orderService.menuOrders = new Array<MenuOrder>();
			this.closeModal();

			this.accountService.getLast5Orders().subscribe(lastOrders => {
				this.accountService.user.last5Orders = lastOrders;
			});
		},
			error => {
				this.error = <any>error;
				this.toasterService.pop('error', 'Failure', error);
				this.isBusy = false;
			}, () => {
				this.isBusy = false;
			});
	}
}