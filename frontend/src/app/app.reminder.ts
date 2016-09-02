import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/configService';

@Component({
	selector: '[reminder]',
	templateUrl: 'app.reminder.html'})

export class ReminderComponent implements OnInit {

	constructor(private configService: ConfigService) { }

	ngOnInit() {
		
	}
}