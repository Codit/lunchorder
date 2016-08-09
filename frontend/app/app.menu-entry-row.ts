import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/configService';
import { MenuEntry } from './domain/dto/menuEntry';
import { MenuRule } from './domain/dto/menuRule';
import { OrderService } from './services/orderService';
import { MenuOrder } from './domain/dto/menuOrder';

@Component({
	selector: '[menu-entry-row]',
	inputs: ['menuEntry'],
	template: `<div class="col-xs-offset-1 col-xs-11">
				<div class="menu-entry-row">
					<span>{{menuEntry.name}}</span>
					<button (click)="openModal()" class="btn btn-primary btn-xs pull-right" style="font-weight:bold;">
					<span style="text-align: left;width: 25px; display: inline-block;">{{menuEntry.price | currency:'EUR':true:'1.0-2'}}</span>
					 <span class="btn-separator"></span>
					 <i class="fa fa-plus"></i></button>
			    </div>
			   </div>
			   <div *ngIf="menuEntry.description" class="col-xs-offset-1 col-xs-10">
				<span style="font-size: 12px;display: inline-block;">{{menuEntry.description}}</span>									
			   </div>
			   
			   <div id="openModal" class="modalDialog active" *ngIf="isModalOpen">
    <div>	
			<i (click)="closeModal()" title="Close" class="fa fa-times close"></i>
        	<h3>Order {{menuEntry.name}}</h3>

			 <form #f="ngForm" class="ui form">
			 	<div class="form-group">
					<div *ngFor="let rule of menuEntry.rules">
						<div class="field">
							<label><input type="checkbox" value="rule" name="{{rule.id}}" [(ngModel)]="rule.isSelected">{{rule.description}} ({{rule.priceDelta | currency:'EUR':true:'1.0-2'}},-)</label>
						</div>
					</div>
					 
					<label for="inputsm">Remarks</label>
					<input class="form-control input-sm" id="inputsm" name="freeText" type="text" [(ngModel)]="menuEntry.freeText">
				</div>


				<button type="button" class="btn btn-primary btn-sm pull-right" style="font-weight:bold;" (click)="addOrder($event, f.value)">Add order to cart</button>
				<button type="button" class="btn btn-sm pull-right" style="font-weight:bold; margin-right:20px;" (click)="closeModal()">Cancel</button>
			 </form>
    </div>
</div>`})

export class MenuEntryRow implements OnInit {

	constructor(private configService: ConfigService, private orderService: OrderService) { }
	menuEntry: MenuEntry;
	isModalOpen: boolean;

	ngOnInit() {
		console.log(`-- menu entryyyy: ${this.menuEntry.name}`);
		console.log(`-- menu rules: ${this.menuEntry.rules}`);
	}

	openModal() {
		this.isModalOpen = true;
		console.log("rules" + this.menuEntry.rules);
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