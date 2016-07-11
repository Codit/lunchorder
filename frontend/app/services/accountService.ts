import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import { Observable } from 'rxjs/Observable';

@Injectable()
export class AccountService {
   constructor(private http: Http) {}

   private accountApiUrl = '/api/accounts';

   getUserProfile (): Observable<api.dto.IGetUserInfoResponse> {
       debugger;
    return this.http.get(`${this.accountApiUrl}`)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

   private extractData(res: Response) {
       console.log(res);
    let body = res.json();
    return body.data || { };
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