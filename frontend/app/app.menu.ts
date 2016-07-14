import { Component, OnInit } from '@angular/core';
import { AdalService } from 'angular2-adal/core';
import { ConfigService } from './services/configService';
import { AccountService } from './services/accountService';
import { MenuService } from './services/menuService';
import { Menu } from './domain/dto/menu';

@Component({
	selector: '[menu]',
	template: `<div class="container">

			<div class="row">

				<div class="col-sm-6 pull-right wow fadeInRightBig">
					<img class="img-responsive " src="css/images/ipad.png" alt="">
				</div>

				<div class="col-sm-6 wow fadeInLeftBig" data-animation-delay="200">
					<h3 class="section-heading">Lunch Menu</h3>
					<div class="sub-title lead3">Lorem ipsum dolor sit atmet sit dolor greand fdanrh<br> sdfs sit atmet sit dolor greand fdanrh sdfs</div>
					<p class="lead">
						In his igitur partibus duabus nihil erat, quod Zeno commuta rest gestiret. Sed virtutem ipsam inchoavit, nihil ampliusuma.
						Scien tiam pollicentur, uam non erat mirum sapientiae lorem cupido patria esse cariorem. Quae qui non vident, nihilamane
						umquam magnum ac cognitione.
					</p>

					<p><a class="btn btn-embossed btn-primary" href="#" role="button">View Details</a>
						<a class="btn btn-embossed btn-info" href="#" role="button">Visit Website</a></p>
				</div>
			</div>
		</div>`})

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