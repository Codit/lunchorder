import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ConfigService } from './configService';
import { Observable } from 'rxjs/Rx';
import { HttpClient } from '../helpers/httpClient';

@Injectable()
export class BalanceService {
   constructor(private http: HttpClient, private configService: ConfigService) {
   }

   private balanceApiUri = `${this.configService.apiPrefix}/balances`;

   putBalance(userId: string, amount: number): Observable<any> {
    return this.http.put(`${this.balanceApiUri}`, { userId: userId, amount: amount })
    .map(this.mapBalance)
      .catch(this.handleError);
  }

    mapBalance = (res: Response): number => {
    let updatedBalance = res.json();
    return updatedBalance;
    }
  
  private handleError(error: any) {
    // In a real world app, we might use a remote logging infrastructure
    // We'd also dig deeper into the error to get a better message
    let errMsg = (error.message) ? error.message :
      error.status ? `${error.status} - ${error.statusText}` : 'Server error';
    console.error(errMsg); // log to console instead
    return Observable.throw(errMsg);
  }
}