import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ConfigService } from './configService';
import { Observable } from 'rxjs/Rx';
import { Menu } from '../domain/dto/menu';
import { MenuRule } from '../domain/dto/menuRule';
import { MenuCategory } from '../domain/dto/menuCategory';
import { HttpClient } from '../helpers/httpClient';

@Injectable()
export class MenuService {
  constructor(private http: HttpClient, private configService: ConfigService) {
  }

  private menuApiUri = `${this.configService.apiPrefix}/menus`;

  getMenu(): Observable<Menu> {
    return this.http.get(`${this.menuApiUri}`)
      .map(this.mapMenu)
      .catch(this.handleError);
  }

  mapMenu = (res: Response): Menu => {
    let body = res.json();
    var menu: Menu;
    menu = new Menu().deserialize(body);
    return menu;
  }
  
  recurseSubCategory = (menu: Menu, menuRule: MenuRule, category: MenuCategory) : void => {
    if (category.subCategories) {
      for (let subCategory of category.subCategories) {
        var menuEntries = menu.entries.filter((menuEntry) => menuEntry.categoryId == subCategory.id);
        for (let menuEntry of menuEntries) {
          // todo this should be added to constructor on mapping
          if (!menuEntry.rules) { menuEntry.rules = new Array<MenuRule>(); }
          menuEntry.rules.push(menuRule);
        }
        this.recurseSubCategory(menu, menuRule, subCategory)
      }
    }
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