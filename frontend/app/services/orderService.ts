import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ConfigService } from './configService';
import { Observable } from 'rxjs/Observable';
import { MenuRule } from '../domain/dto/menuRule';
import { MenuOrder } from '../domain/dto/menuOrder';
import { HttpClient } from '../helpers/httpClient';

@Injectable()
export class OrderService {
  constructor(private http: HttpClient, private configService: ConfigService) {
    this.menuOrders = new Array<MenuOrder>();
  }

  private menuApiUri = `${this.configService.apiPrefix}/orders`;
  menuOrders : MenuOrder[];

  totalPrice() : number {
    var price = 0;
    for (let menu of this.menuOrders) {
      debugger;
      price += menu.price;
      for (let rule of menu.appliedMenuRules)
      {
        price += rule.priceDelta;
      }
    }
    return price;
  }

  postMenuOrders(): Observable<any> {
    return this.http.post(`${this.menuApiUri}`, { "menuOrders": this.menuOrders })
      .catch(this.handleError);
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