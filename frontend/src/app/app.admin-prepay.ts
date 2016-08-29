import { Component, OnInit } from '@angular/core';
import { BalanceService } from './services/balanceService';
import { AccountService } from './services/accountService';
import { PlatformUserListItem } from './domain/dto/platformUserListItem';
import { ToasterService } from 'angular2-toaster/angular2-toaster';
import { REACTIVE_FORM_DIRECTIVES, FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { Validators } from '@angular/common';

@Component({
	selector: '[admin-prepay]',
	directives: [REACTIVE_FORM_DIRECTIVES],
	template: `<div class="container">
			<div class="row">
				<div class="col-md-6 col-md-offset-3 text-center wrap_title ">
					<h2>Manage prepays</h2>
					<p class="lead" style="margin-top:0">Add funds to user accounts.</p>
						<form novalidate (ngSubmit)="addBalance()" #prepayForm="ngForm">
					
					<div class="row">
						<div class="col-md-6">
							<i class="fa fa-user"></i>
							<select [(ngModel)]="selectedUser" required name="selectedUser">
								<option *ngFor="let user of users" [ngValue]="user">{{user.getName()}}</option>
							</select>
						</div>
						<div class="col-md-6">
							<div class="field">
								<i class="fa fa-eur"></i>
								<label><input type="text" [(ngModel)]="balanceAmount" name="balanceAmount" required (keyup)="replaceComma()"></label>
							</div>
							<button type="button" type="submit" class="btn btn-primary btn-sm pull-right" style="font-weight:bold;" [disabled]="isBusy || !prepayForm.valid">					
							<i class="fa fa-spinner fa-spin" *ngIf="isBusy"></i> Add balance for user</button>
						</div>
					</div>
							</form>
					
				</div>
			</div>
		</div>`})

export class AdminPrepayComponent implements OnInit {
	constructor(private accountService: AccountService, private balanceService: BalanceService, private toasterService: ToasterService) {
	}
	userBalanceError: string;
	users: PlatformUserListItem[];
	isBusy: boolean;
	user: PlatformUserListItem;
	balanceAmount: string;
	selectedUser: PlatformUserListItem;

	ngOnInit() {
		this.accountService.getAllUsers().subscribe(
			response => {
				this.users = response.users
			});
	}

	replaceComma() {
		if (this.balanceAmount) {
			var hasComma = this.balanceAmount.indexOf(',') > 0;
			if (hasComma) {
				this.balanceAmount = this.balanceAmount.replace(',', '.');
			}
		}
	}

	addBalance() {
		debugger;
		this.isBusy = true;
		this.userBalanceError = "";

		this.balanceService.putBalance(this.selectedUser.userId, this.balanceAmount).subscribe(
			newBalance => {
				this.toasterService.pop('success', 'Success', `Balance updated to ${newBalance}`);
				this.user = null;
				this.balanceAmount = null;
				this.isBusy = false;
			},
			error => {
				this.userBalanceError = <any>error,
					this.toasterService.pop('error', 'Failure', 'Something went wrong');
				this.isBusy = false;
			});
	};
}