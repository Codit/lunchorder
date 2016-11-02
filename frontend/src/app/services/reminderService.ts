import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ConfigService } from './configService';
import { Observable } from 'rxjs/Rx';
import { Reminder } from '../domain/dto/reminder';
import { HttpClient } from '../helpers/httpClient';
import { ErrorHandlerService } from './errorHandlerService';

@Injectable()
export class ReminderService {
  constructor(private http: HttpClient, private configService: ConfigService, private errorHandlerService: ErrorHandlerService) {  }

  private reminderApiUri = `${this.configService.apiPrefix}/reminders`;

  saveReminder(reminder: Reminder): Observable<Reminder> {
    return this.http.post(`${this.reminderApiUri}`, reminder)
      .catch(this.errorHandlerService.handleError);
  }

  deleteReminder(reminderType: number) {
return this.http.delete(`${this.reminderApiUri}?type=${reminderType}`, )
      .catch(this.errorHandlerService.handleError);
  }
}