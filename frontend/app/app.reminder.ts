import { Component, OnInit } from '@angular/core';
import { AdalService } from 'angular2-adal/core';
import { ConfigService } from './services/configService';

@Component({
	selector: '[reminder]',
	template: `<div class="container">
			<div class="row">
				<div class="col-md-6 col-md-offset-3 text-center white">
					<h2>Get Reminders</h2>
					<p class="lead" style="margin-top:0">Never miss an order.</p>
				</div>
				<div class="col-md-6 col-md-offset-3 text-center">
					<div class="mockup-content">
						<div class="morph-button wow pulse morph-button-inflow morph-button-inflow-1">
							<button type="button "><span>Subscribe to our Newsletter</span></button>
							<div class="morph-content">
								<div>
									<div class="content-style-form content-style-form-4 ">
										<h2 class="morph-clone">Subscribe to our Newsletter</h2>
										<form>
											<p><label>Your Email Address</label><input type="text" /></p>
											<p><button>Remind me</button></p>
										</form>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>>
		</div>`})

export class ReminderComponent implements OnInit {

	constructor(private configService: ConfigService) { }

	ngOnInit() {
		
	}
}