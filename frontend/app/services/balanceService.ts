import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ConfigService } from './configService';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '../helpers/httpClient';

@Injectable()
export class BalanceService {
   constructor(private http: HttpClient, private configService: ConfigService) {
   }

   private balanceApiUri = `${this.configService.apiPrefix}/balances`;
}