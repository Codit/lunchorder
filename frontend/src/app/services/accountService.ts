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
import { Subject, ReplaySubject }    from 'rxjs';

@Injectable()
export class AccountService {
  constructor(private http: Http, private httpClient: HttpClient, private configService: ConfigService, 
  private tokenHelper: TokenHelper, private errorHandlerService: ErrorHandlerService) {
     this._isAuthenticated$ = <Subject<boolean>>new Subject();
     this._isAuthenticated$.next(false);

    var token = this.tokenHelper.getToken();

    if (token) {
        this.getUserProfile().subscribe(
          userInfo => {
            this.userSource.next(userInfo);
            if (userInfo.userToken) {
              this.tokenHelper.authToken = userInfo.userToken;
            history.pushState("", document.title, window.location.pathname);
              this._isAuthenticated$.next(true);
            }
          },
          error => this.userInfoError = <any>error);
    }
  }
  
  private userSource = new ReplaySubject<GetUserInfoResponse>(1);
    public user$ = this.userSource.asObservable();


get isAuthenticated$() : Observable<boolean>  {
    return this._isAuthenticated$.asObservable();
  }

  private _isAuthenticated$: Subject<boolean>; 
  private accountApiUrl = `${this.configService.apiPrefix}/accounts`;
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
    
    var response = new GetUserInfoResponse().deserialize(userObject);
    this.tokenHelper.authToken =  body.access_token;

    debugger;
    this._isAuthenticated$.next(true);
    this.userSource.next(response);

    return body || {};
  }

  login() {
    var currentUrl = document.location.protocol + "//" + document.location.host;
    var stateId = Math.random().toString(36).substring(7);
    var nonce = Math.random().toString(36).substring(7);
    window.location.href = `https://login.microsoftonline.com/${this.configService.adalConfig.tenant}/oauth2/authorize?response_type=id_token&client_id=${this.configService.adalConfig.clientId}&redirect_uri=${currentUrl}&state=${stateId}&nonce=${nonce}`
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

  private extractLoginData = (res: Response) => {
    let body = res.json();
    debugger;
    this.userSource.next(new GetUserInfoResponse().deserialize(body));
    return body || {};
  }
}