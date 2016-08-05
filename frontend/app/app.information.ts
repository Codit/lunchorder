import { Component, OnInit } from '@angular/core';
import { AdalService } from 'angular2-adal/core';
import { ConfigService } from './services/configService';
import { AccountService } from './services/accountService';
import { MenuService } from './services/menuService';
import { Menu } from './domain/dto/menu';

@Component({
	selector: '[information]',
	template: `
		<div class="container">
			<div class="row">
				<div class="col-sm-4  wow fadeInDown text-center">
					<img class="rotate" src="css/images/icon/watch.svg" alt="Generic placeholder image">
					<h3>On Time</h3>
					<p class="lead">Orders are sent to the venue @ {{menu?.vendor?.submitOrderTime}}. Late orders are your own responsibility and should be made by giving them a call. </p>
				</div>
				<div class="col-sm-4 wow fadeInDown text-center">
					<img class="rotate" src="css/images/icon/map.svg" alt="Generic placeholder image">
					<h3>Venue</h3>
					<p class="lead">{{menu?.vendor?.name}} </p>
					<p>Phone: {{menu?.vendor?.address?.phone}} </p>
					<p *ngIf="menu?.vendor?.address?.fax">Fax: {{menu?.vendor?.address?.fax}}</p>
				</div>

				<div class="col-sm-4 wow fadeInDown text-center">
					<img class="rotate" src="css/images/icon/savings.svg" alt="Generic placeholder image">
					<h3>Payment</h3>
					<p class="lead">Payment is done upfront. Please consult the responsible to add funds to your account.</p>
				</div>
			</div>
		</div>
`})

export class InformationComponent implements OnInit {

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