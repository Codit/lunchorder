import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { ConfigService } from '../services/configService';

@Injectable()
export class HttpClient {
    constructor(private http: Http, private configService: ConfigService) {
    }

    createAuthorizationHeader(headers: Headers) {
        headers.append('Authorization',`Bearer ${this.configService.authToken}`);
    }

    get(url: string) {
        let headers = new Headers();
        this.createAuthorizationHeader(headers);
        return this.http.get(url, {
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
}
