import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { TokenHelper } from './tokenHelper';
import { URLSearchParams } from '@angular/http';

@Injectable()
export class HttpClient {
    constructor(private http: Http, private tokenHelper: TokenHelper) {
    }

    createAuthorizationHeader(headers: Headers) {
        headers.append('Authorization',`Bearer ${this.tokenHelper.authToken}`);
    }

    get(url: string) {
        let headers = new Headers();
        this.createAuthorizationHeader(headers);
        return this.http.get(url, {
        headers: headers
        });
    }

    getWithQuery(url: string, querystring : any) {
        let headers = new Headers();
        this.createAuthorizationHeader(headers);
        return this.http.get(url, {
            search: new URLSearchParams(querystring),
            headers: headers
        });
    }

    post(url: string, data: any) {
        let headers = new Headers();
        this.createAuthorizationHeader(headers);
        return this.http.post(url, data, {
        headers: headers
        });
    }

    put(url: string, data: any) {
        let headers = new Headers();
        this.createAuthorizationHeader(headers);
        return this.http.put(url, data, {
        headers: headers
        });
    }
}
