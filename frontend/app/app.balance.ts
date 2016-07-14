import { Component, OnInit } from '@angular/core';
import { AdalService } from 'angular2-adal/core';
import { ConfigService } from './services/configService';
import { BalanceService } from './services/balanceService';

@Component({
	selector: '[balance]',
	template: `<div class="container">
			<div class="row">
				<div class="col-md-6 col-md-offset-3 text-center wrap_title ">
					<h2>Balance</h2>
					<p class="lead" style="margin-top:0">Check your balance and other options.</p>
				</div>
			</div>
		</div>`})

export class BalanceComponent implements OnInit {

	constructor(private configService: ConfigService, private balanceService: BalanceService) { }

	ngOnInit() {

	}
}