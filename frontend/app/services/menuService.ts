import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ConfigService } from './configService';
import { Observable } from 'rxjs/Observable';
import { Menu } from '../domain/dto/menu';


@Injectable()
export class MenuService {
   constructor(private http: Http, private configService: ConfigService) {
     automapper.createMap('{}', 'Menu');
   }

   private menuApiUri = `${this.configService.apiPrefix}/menus`;

   getMenu (): Observable<app.domain.dto.IMenu> {
    return this.http.get(`${this.menuApiUri}`)
                    .map(this.mapMenu)
                    .catch(this.handleError);
  }

   private mapMenu(res: Response) : Menu {
     
     debugger;
       console.log(res);
    let body = res.json();
    var menu: Menu = automapper.map('{}', 'Menu', body);
    return menu;
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