import { Component, OnInit } from '@angular/core';
import { AdalService } from 'angular2-adal/core';
import { ConfigService } from './services/configService';
import { AccountService } from './services/accountService';
import { MenuService } from './services/menuService';
import { Menu } from './domain/dto/menu';
import { MenuCategoryRow } from './app.menu-category-row';
import { MenuEntry } from './domain/dto/menuEntry';
<<<<<<< HEAD
import { StickCartDirective } from './directives/stickCartDirective';

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
			<div class="row">
				<div class="col-xs-9 wow fadeInLeftBig" data-animation-delay="200">
					<div menu-category-row class="col-xs-12" *ngFor="let cat of menu?.categories" [category]="cat" [menuEntries]="menu?.entries"></div>
				</div>
				<div stick-cart-rx id="cart" class="col-xs-3 wow fadeInRightBig" data-animation-delay="200">
					<div style="width: 200px; height: 200px; background: #cecece; border-radius: 50%;">
						<i class="fa fa-shopping-basket" style="font-size: 82px; vertical-align: middle; padding: 20px 55px;
	-ms-transform: rotate(18deg); /* IE 9 */ -webkit-transform: rotate(18deg); /* Chrome, Safari, Opera */ transform: rotate(-18deg);">
		<div class="badge badge-info" style="font-size: 20px; padding: 5px 10px; margin: -90px 0px 0px 67px; transform: rotate(18deg);">0</div>
	</i>

  <div style="left: auto; text-align: center; color: #373d50; font-weight: bold; font-size: 32px; margin-top: -85px;">
&euro; 5.00,-
  </div>
</div></div>
				</div>
=======

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
>>>>>>> master
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