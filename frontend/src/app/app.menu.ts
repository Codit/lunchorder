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
	template: `<div class="container">
			<div class="row">
				<div class="col-md-6 col-md-offset-3 text-center wrap_title ">
					<h2>Lunch Menu</h2>
					<p class="lead" style="margin-top:0">Make an order using the following menu.</p>
				</div>
			</div>
			<div class="row" *ngIf="!menu || isClosed">
				<div *ngIf="isBusyMenu"><i class="fa fa-spinner spin"></i></div>
				<div *ngIf="!isBusyMenu && !menu && !isClosed" class="alert alert-warning">There is currently no active menu</div>
				<div *ngIf="isClosed" class="alert alert-warning">Sorry, the vendor is closed today.</div>
			</div>
			<div class="row" *ngIf="menu?.entries && !isClosed">
				<div class="col-xs-9 wow fadeInLeftBig" data-animation-delay="200">
					<div menu-category-row class="col-xs-12 col-md-6" *ngFor="let cat of menu?.categories" [category]="cat" [menuEntries]="menu?.entries"></div>
				</div>
				<div style="cursor:pointer;" (click)="openCheckout()" stick-cart-rx id="cart" class="col-xs-12 col-md-3" data-animation-delay="200">
					<div style="width: 200px; height: 200px; background: #cecece; border-radius: 50%;">
						<i class="fa fa-shopping-basket" style="font-size: 82px; vertical-align: middle; padding: 20px 55px;
	-ms-transform: rotate(18deg); /* IE 9 */ -webkit-transform: rotate(18deg); /* Chrome, Safari, Opera */ transform: rotate(-18deg);">
		<div class="badge badge-info" style="font-size: 20px; padding: 5px 10px; margin: -90px 0px 0px 67px; transform: rotate(18deg);">{{orderService.menuOrders.length}}</div>
	</i>

  <div style="left: auto; text-align: center; color: #373d50; font-weight: bold; font-size: 32px; margin-top: -85px;">
{{orderService.totalPrice() | currency:'EUR':true:'1.2-2' }},-
  </div>
</div></div>
				</div>
				
				<div class="modalDialog active checkout" *ngIf="isModalOpen">
    <div>	
			<i (click)="closeModal()" title="Close" class="fa fa-times close"></i>
			<i class="fa fa-shopping-basket basket" title="Checkout"></i>
        	
			<div><h4>Your order </h4><h4 class="right">Price <i class="fa fa-trash-o" style="cursor:pointer" title="remove all items" (click)="removeOrders(orderService.menuOrders)"></i></h4></div>
<div>
					<div *ngFor="let menuOrder of orderService.menuOrders" class="item">
						<span class="right">{{menuOrder.price | currency:'EUR':true:'1.2-2' }} <i class="fa fa-trash-o" style="cursor:pointer" title="remove item" (click)="removeOrder(menuOrder)"></i></span>
						<span>{{menuOrder.name}}</span>
						<div *ngFor="let rule of menuOrder.appliedMenuRules">
							<span class="description">{{rule.description}}</span>
						</div>
						<span class="description" [hidden]="!menuOrder.freeText">{{menuOrder.freeText}}</span>
					</div>
					<div class="right">
						Total: <span>{{orderService.totalPrice() | currency:'EUR':true:'1.2-2' }}</span>
					</div>
					<div class="clear right" [class.negative]="getBalance() < 0" [class.positive]="getBalance() >= 0">
						Balance after purchase: <span>{{ getBalance() | currency:'EUR':true:'1.2-2' }}</span>
					</div>

					<div class="clear">
						<button type="button" class="btn btn-primary btn-sm pull-right" style="font-weight:bold;" [disabled]="isBusy || getBalance() < 0 || orderService.menuOrders.length <= 0" (click)="finalizeOrder()"><i class="fa fa-spinner fa-spin" *ngIf="isBusy"></i> Buy now</button>
					</div>
    </div>
</div>`
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