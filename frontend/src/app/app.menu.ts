import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/config.service';
import { AccountService } from './services/account.service';
import { MenuService } from './services/menu.service';
import { Menu } from './domain/dto/menu';
import { MenuCategoryRow } from './app.menu-category-row';
import { MenuEntry } from './domain/dto/menuEntry';
import { MenuOrder } from './domain/dto/menuOrder';
import { OrderService } from './services/order.service';
import { ToasterService } from 'angular2-toaster/angular2-toaster';
import { GetUserInfoResponse } from './domain/dto/getUserInfoResponse'
import { BadgeService } from './services/badge.service';

@Component({
	selector: '[menu]',
	templateUrl: 'app.menu.html'
})

export class MenuComponent implements OnInit {

	// todo move orderservice button to other component.
	constructor(private configService: ConfigService, private menuService: MenuService, private orderService: OrderService, private accountService: AccountService, private toasterService: ToasterService, private badgeService: BadgeService) {
		this.accountService.user$.subscribe(user => {
			this.user = user;
		});
	 }

	 user: GetUserInfoResponse;
	isModalOpen: boolean;
	menu: Menu;
	// todo, inspect error object.
	error: any;
	isBusy: boolean;
	isBusyMenu: boolean = true;
	isClosed: boolean = false;

	ngOnInit() {
		var dayOfWeek = new Date().getDay();

		this.isClosed = ((dayOfWeek == 6) || (dayOfWeek == 0)) && !this.configService.allowWeekendOrders;

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
				(error : any) => this.error = <any>error);
		}
	}

	getBalance(): number {
		return this.user.balance - this.orderService.totalPrice();
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
			console.log("current balance: " + this.user.balance);
			console.log("total price: " + this.orderService.totalPrice())
			this.user.balance -= this.orderService.totalPrice();
			console.log("differnt balance: " + this.user.balance);
			this.orderService.menuOrders = new Array<MenuOrder>();
			this.closeModal();

			this.accountService.getLast5Orders().subscribe(lastOrders => {
				this.user.last5Orders = lastOrders;
			});

			this.badgeService.postOrderBadge().subscribe(badgeTexts => {
				badgeTexts.forEach(x => this.toasterService.pop('success', 'Badge', x));
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