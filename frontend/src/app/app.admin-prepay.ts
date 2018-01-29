import { Component, OnInit } from '@angular/core';
import { BalanceService } from './services/balance.service';
import { AccountService } from './services/account.service';
import { PlatformUserListItem } from './domain/dto/platformUserListItem';
import { ToasterService } from 'angular2-toaster/angular2-toaster';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { BadgeService } from './services/badge.service';

@Component({
	selector: '[admin-prepay]',
	templateUrl: 'app.admin-prepay.html'
})

export class AdminPrepayComponent implements OnInit {
	constructor(private accountService: AccountService, private balanceService: BalanceService, private toasterService: ToasterService, private badgeService: BadgeService) {
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

	onUserChange(formUser: PlatformUserListItem) {
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
		this.isBusy = true;
		this.userBalanceError = "";

		this.balanceService.putBalance(this.selectedUser.userId, this.balanceAmount).subscribe(
			newBalance => {
				this.badgeService.postPrepayBadge(this.selectedUser.userName).subscribe(badgeTexts => {
					badgeTexts.forEach(x => this.toasterService.pop('success', 'Badge', x));
				});

				this.toasterService.pop('success', 'Success', `Balance updated to ${newBalance}`);
				this.user = null;
				this.balanceAmount = null;
				this.isBusy = false;
				this.onUserChange(this.selectedUser);

			},
			error => {
				this.userBalanceError = <any>error,
					this.toasterService.pop('error', 'Failure', this.userBalanceError);
				this.isBusy = false;
			});
	};
}