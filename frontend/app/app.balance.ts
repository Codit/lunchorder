import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/accountService';
import { BalanceService } from './services/balanceService';

@Component({
	selector: '[balance]',
	template: `<div class="container">
			<div class="row">
				<div class="col-md-6 col-md-offset-3 text-center wrap_title ">
					<h2>About you</h2>
					<p class="lead" style="margin-top:0">Check your balance and last 5 orders.</p>
					<div class="row">
						<div class="col-md-6">
							<p><i class="fa fa-user" style="padding-right: 13px;padding-left: 7px;font-size: 24px;"></i>{{accountService.user.userName}}</p>
						</div>
						<div class="col-md-6">
							<p><i class="fa fa-balance-scale" style="padding-right: 10px;font-size: 24px;"></i>{{accountService.user.balance | currency:'EUR':true:'1.2-2'}}</p>
						</div>
					</div>
					<div class="row">
						<table class="table history table-striped">
							<thead>
								<tr>
									<th>Date</th>
									<th>Name</th>
									<th>Total Price</th>
								</tr>
							</thead>
							<tbody>
								<tr *ngFor="let lastOrder of accountService.user.last5Orders">
									<td>{{lastOrder.orderTime | date: 'medium'}}</td>
									<td>
										
											<div *ngFor="let entry of lastOrder.lastOrderEntries">
												<span>{{ entry.name }} ({{entry.price | currency:'EUR':true:'1.2-2' }})</span>
												<span class="detail">{{ entry.appliedRules }}</span>
											</div>
									</td>
									<td>{{ lastOrder.finalPrice | currency:'EUR':true:'1.2-2' }}</td>
								</tr>
							</tbody>
						</table>
					</div>
				</div>
			</div>
		</div>`})

export class BalanceComponent implements OnInit {

	constructor(private balanceService: BalanceService, private accountService : AccountService) { }

	ngOnInit() {
		
	}
}