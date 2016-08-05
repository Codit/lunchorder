import { Component, OnInit } from '@angular/core';
import { BalanceService } from './services/balanceService';
import { AccountService } from './services/accountService';
import { PlatformUserListItem } from './domain/dto/platformUserListItem';

@Component({
	selector: '[admin-prepay]',
	template: `<div class="container">
			<div class="row">
				<div class="col-md-6 col-md-offset-3 text-center wrap_title ">
					<h2>Manage prepays</h2>
					<i class="fa fa-user"></i>
					<select [(ngModel)]="selectedUser">
    					<option *ngFor="let user of users" [ngValue]="user">{{user.getName()}}</option>
					</select>
					<i class="fa fa-eur"></i>
					<div class="field">
						<label><input type="text" [(ngModel)]="balanceAmount"></label>
					</div>
					<button type="button" class="btn btn-primary btn-sm pull-right" style="font-weight:bold;"  [disabled]="isBusy" (click)="addBalance(selectedUser, balanceAmount)">
					<i class="fa fa-spinner fa-spin" *ngIf="isBusy"></i> Add balance for user</button>
					<span *ngIf="userBalance">Updated userbalance, user has now {{userBalance}}</span>
				</div>
			</div>
		</div>`})

export class AdminPrepayComponent implements OnInit {
	constructor(private accountService: AccountService, private balanceService: BalanceService) { }
	userBalance: number;
	userBalanceError: string;
	users: PlatformUserListItem[];
	isBusy: boolean;

	ngOnInit() {
		this.accountService.getAllUsers().subscribe(
			response => {
				this.users = response.users
			});
	}

	addBalance(selectedUser: PlatformUserListItem, balanceAmount: number) {
		this.isBusy = true;
		this.userBalance = null;
		this.userBalanceError = "";

		debugger;
		this.balanceService.putBalance(selectedUser.userId, balanceAmount).subscribe(
			newBalance => {
				this.userBalance = newBalance

			},
			error => this.userBalanceError = <any>error,
			() => {
				this.isBusy = false;
			});
	};
}