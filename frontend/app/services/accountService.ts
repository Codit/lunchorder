import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ConfigService } from './configService';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '../helpers/httpClient';
import { GetUserInfoResponse } from '../domain/dto/getUserInfoResponse'
import { GetAllUsersResponse } from '../domain/dto/getAllUsersResponse'
import { AdalService } from 'angular2-adal/core';
import { TokenHelper } from '../helpers/tokenHelper';

@Injectable()
export class AccountService {
  constructor(private http: HttpClient, private configService: ConfigService, private adalService: AdalService, private tokenHelper: TokenHelper) {
    this.adalService.init(this.configService.adalConfig);
    console.log('ctor accountservice');
    this.adalService.handleWindowCallback();
    if (this.adalService) {
      if (this.adalService.userInfo.isAuthenticated) {
        this.tokenHelper.getToken();

        this.getUserProfile().subscribe(
          userInfo => {
          this.user = userInfo;
            if (userInfo.userToken) {
              this.isAuthenticated = true;
              this.tokenHelper.authToken = userInfo.userToken;
            }
          },
          error => this.userInfoError = <any>error);
      }
    }
  }

 private _isAuthenticated:boolean = false;
    get isAuthenticated():boolean {
        return this._isAuthenticated;
    }
    set isAuthenticated(isAuthenticated:boolean) {
        this._isAuthenticated = isAuthenticated;
    }

  private accountApiUrl = `${this.configService.apiPrefix}/accounts`;

private _user:GetUserInfoResponse = new GetUserInfoResponse();
    get user():GetUserInfoResponse {
        return this._user;
    }
    set user(user:GetUserInfoResponse) {
        this._user = user;
    }


  userInfoError: any;

  login() {
    this.adalService.login();
  }

  getUserProfile(): Observable<GetUserInfoResponse> {
    return this.http.get(`${this.accountApiUrl}`)
      .map(this.extractData)
      .catch(this.handleError);
  }
  
  getAllUsers(): Observable<GetAllUsersResponse> {
    return this.http.get(`${this.accountApiUrl}/users`)
      .map(this.extractUsers)
      .catch(this.handleError);
  }

private extractUsers(res: Response) {
    console.log(res);
    let body = res.json();
    return new GetAllUsersResponse().deserialize(body);
  }

  private extractData(res: Response) {
    console.log(res);
    let body = res.json();
    this.user = new GetUserInfoResponse().deserialize(body);
    return body || {};
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