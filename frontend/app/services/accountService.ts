import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ConfigService } from './configService';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '../helpers/httpClient';

@Injectable()
export class AccountService {
   constructor(private http: HttpClient, private configService: ConfigService) {}

   private accountApiUrl = `${this.configService.apiPrefix}/accounts`;

   getUserProfile (): Observable<app.domain.dto.IGetUserInfoResponse> {
    return this.http.get(`${this.accountApiUrl}`)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

   private extractData(res: Response) {
       console.log(res);
    let body = res.json();
    return body || { };
  }
  private handleError (error: any) {
    // In a real world app, we might use a remote logging infrastructure
    // We'd also dig deeper into the error to get a better message
    let errMsg = (error.message) ? error.message :
      error.status ? `${error.status} - ${error.statusText}` : 'Server error';
    console.error(errMsg); // log to console instead
    return Observable.throw(errMsg);
  }
}