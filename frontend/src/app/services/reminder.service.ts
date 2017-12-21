import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ConfigService } from './config.service';
import { Observable } from 'rxjs/Rx';
import { Reminder } from '../domain/dto/reminder';
import { HttpClient } from '../helpers/httpClient';
import { ErrorHandlerService } from './error-handler.service';
import  { PostReminderRequest } from '../domain/dto/postReminderRequest';

@Injectable()
export class ReminderService {
  constructor(private http: HttpClient, private configService: ConfigService, private errorHandlerService: ErrorHandlerService) {  }

  private reminderApiUri = `${this.configService.apiPrefix}/reminders`;

  saveReminder(reminder: Reminder): Observable<Reminder> {
    var request = new PostReminderRequest().deserialize({ reminder: reminder });
    return this.http.post(`${this.reminderApiUri}`, request)
      .catch(this.errorHandlerService.handleError);
  }

  deleteReminder(reminderType: number) {
return this.http.delete(`${this.reminderApiUri}?type=${reminderType}`, )
      .catch(this.errorHandlerService.handleError);
  }
}