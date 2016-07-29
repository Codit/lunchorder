import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/configService';
import { MenuEntry } from './domain/dto/menuEntry';
import { MenuRule } from './domain/dto/menuRule';

@Component({
	selector: '[menu-entry-row]',
	inputs: ['menuEntry'],
	template: `<div class="col-xs-offset-1 col-xs-11">
				<div class="menu-entry-row">
					<span>{{menuEntry.name}}</span>
					<button (click)="openModal()" class="btn btn-primary btn-xs pull-right" style="font-weight:bold;">&euro; {{menuEntry.price}}
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
			<div *ngFor="let rule of menuEntry.rules">
			<div>
				<label><input type="checkbox" value="">{{rule.description}} (&euro; {{rule.priceDelta}},-)</label>
				</div>
			</div>
			<div class="form-group">
				<label for="inputsm">Remarks</label>
				<input class="form-control input-sm" id="inputsm" type="text">
			</div>
        <button class="btn btn-primary btn-sm pull-right" style="font-weight:bold;" (click)="addOrder()">Add order to cart</button>
    </div>
</div>`})
	 
export class MenuEntryRow implements OnInit {

	constructor(private configService: ConfigService) { }
	menuEntry: MenuEntry;
	isModalOpen: boolean;
	menuRules: MenuRule[];

	ngOnInit() {
		console.log(`-- menu entryyyy: ${this.menuEntry.name}`)
	}

	openModal() {
		this.isModalOpen = true;
		console.log("rules" + this.menuEntry.rules);
	}

	addOrder() {
		this.closeModal();
	}

	closeModal() {
		this.isModalOpen = false;
	}
}