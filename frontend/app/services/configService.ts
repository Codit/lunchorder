import {Injectable} from '@angular/core';

@Injectable()
export class ConfigService {
    public get adalConfig(): any {
        return {
            tenant: 'codit.onmicrosoft.com',
            clientId: 'f2477cb6-a5a2-40d1-8ba2-736ffd224519',
            redirectUri: window.location.origin + '/',
            postLogoutRedirectUri: window.location.origin + '/'
        };
    }
}