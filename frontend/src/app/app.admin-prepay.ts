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
	templateUrl: 'app.admin-prepay.html'})

export class AdminPrepayComponent implements OnInit {
	constructor(private accountService: AccountService, private balanceService: BalanceService, private toasterService: ToasterService) {
	}
	userBalanceError: string;
	users: PlatformUserListItem[];
	isBusy: boolean;
	isBusyHistory: boolean = false;
	user: PlatformUserListItem;
	balanceAmount: string;
	selectedUser: PlatformUserListItem;
	history: any; // todo change to typed.

	ngOnInit() {
		this.accountService.getAllUsers().subscribe(
			response => {
				this.users = response.users
			});
	}

	onUserChange(formUser : PlatformUserListItem) {
		this.history = null;
		this.isBusyHistory = true;
		this.balanceService.getHistory(formUser.userId).subscribe(
			history => {
				this.history = history;
				this.isBusyHistory = false;
			},
			error => {
				this.userBalanceError = <any>error,
					this.toasterService.pop('error', 'Failure', 'Something went wrong');
				this.isBusyHistory = false;
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
				this.onUserChange(this.selectedUser);
			},
			error => {
				this.userBalanceError = <any>error,
					this.toasterService.pop('error', 'Failure', 'Something went wrong');
				this.isBusy = false;
			});
	};
}