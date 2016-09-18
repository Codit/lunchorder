import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { ConfigService } from './configService';
import { Observable } from 'rxjs/Rx';
import { HttpClient } from '../helpers/httpClient';
import { GetUserInfoResponse } from '../domain/dto/getUserInfoResponse'
import { GetAllUsersResponse } from '../domain/dto/getAllUsersResponse'
import { TokenHelper } from '../helpers/tokenHelper';
import { LastOrder } from '../domain/dto/lastOrder'
import { LoginForm } from '../domain/dto/loginForm'
import { ErrorHandlerService } from './errorHandlerService';
import { Subject }    from 'rxjs/Subject';

@Injectable()
export class AccountService {
  constructor(private http: Http, private httpClient: HttpClient, private configService: ConfigService, 
  private tokenHelper: TokenHelper, private errorHandlerService: ErrorHandlerService) {
     this._isAuthenticated$ = <Subject<boolean>>new Subject();
     this._isAuthenticated$.next(false);

    // todo enable adal
    // this.adalService.init(this.configService.adalConfig);
    // this.adalService.handleWindowCallback();
    // if (this.adalService) {
    //   if (this.adalService.userInfo.isAuthenticated) {
    var token = this.tokenHelper.getToken();

    if (token) {
        this.getUserProfile().subscribe(
          userInfo => {
            this.user = userInfo;
            if (userInfo.userToken) {
              this.tokenHelper.authToken = userInfo.userToken;
            history.pushState("", document.title, window.location.pathname);
              this._isAuthenticated$.next(true);
            }
          },
          error => this.userInfoError = <any>error);
    }
  }
  

get isAuthenticated$() : Observable<boolean>  {
    return this._isAuthenticated$.asObservable();
  }

  private _isAuthenticated$: Subject<boolean>; 


  // private _isAuthenticated = new Subject<boolean>();
  // get isAuthenticated(): Subject<boolean> {
  //   return this._isAuthenticated.asObservable();
  // }
  // set isAuthenticated(isAuthenticated: Subject<boolean>) {
  //   this._isAuthenticated = isAuthenticated;
  // }

  private accountApiUrl = `${this.configService.apiPrefix}/accounts`;

  private _user: GetUserInfoResponse = new GetUserInfoResponse();
  get user(): GetUserInfoResponse {
    return this._user;
  }
  set user(user: GetUserInfoResponse) {
    this._user = user;
  }

  userInfoError: any;

  loginUserPassword(loginForm: LoginForm) : Observable<any> {
    var headers = new Headers();
    headers.append('Content-Type', 'application/x-www-form-urlencoded');

    return this.http.post(`${this.configService.apiPrefix}/oauth/token`, loginForm.createPayload(), {
      headers: headers
    })
      .map(this.mapToLoginResponse)
      .catch(this.errorHandlerService.handleError);
  }

  mapToLoginResponse = (res : any) => {
    let body = res.json();
    var userObject = JSON.parse(body.payload);
    
    this.user = new GetUserInfoResponse().deserialize(userObject);
    this.user.userToken = body.access_token;
    this.tokenHelper.authToken = this.user.userToken;
    this._isAuthenticated$.next(true);
    return body || {};
  }

  login() {
    var currentUrl = window.location.href; 
    var stateId = Math.random().toString(36).substring(7);
    var nonce = Math.random().toString(36).substring(7);
    window.location.href = `https://login.microsoftonline.com/codit.onmicrosoft.com/oauth2/authorize?response_type=id_token&client_id=${this.configService.adalConfig.clientId}&redirect_uri=${currentUrl}&state=${stateId}&nonce=${nonce}`
  }

  getUserProfile(): Observable<GetUserInfoResponse> {
    return this.httpClient.get(`${this.accountApiUrl}`)
      .map(this.extractLoginData)
      .catch(this.errorHandlerService.handleError);
  }

  getLast5Orders(): Observable<LastOrder[]> {
    return this.httpClient.get(`${this.accountApiUrl}/last5Orders`)
      .map(this.extractLastOrders)
      .catch(this.errorHandlerService.handleError);
  }

  getAllUsers(): Observable<GetAllUsersResponse> {
    return this.httpClient.get(`${this.accountApiUrl}/users`)
      .map(this.extractUsers)
      .catch(this.errorHandlerService.handleError);
  }

  private extractLastOrders(res: Response) {
    let body = res.json();

    var last5Orders = new Array<LastOrder>();
    for (var lastOrder of body) {
      last5Orders.push(new LastOrder().deserialize(lastOrder));
    }

    return last5Orders;
  }

  private extractUsers(res: Response) {
    let body = res.json();
    return new GetAllUsersResponse().deserialize(body);
  }

  private extractLoginData(res: Response) {
    let body = res.json();
    this.user = new GetUserInfoResponse().deserialize(body);
    return body || {};
  }
}