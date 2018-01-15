import {Injectable} from '@angular/core';

@Injectable()
export class ConfigService {
    public get adalConfig(): any {
        return {
            tenant: '____configTenant____',
            clientId: '____configClientId____',
            redirectUri: window.location.origin + '/',
            postLogoutRedirectUri: window.location.origin + '/'
        };
    }

    public get authConfig() : any {
        return {
            activeDirectoryEnabled: '____activeDirectoryEnabled____',
            usernamePasswordEnabled: '____usernamePasswordEnabled____'
        }
    }

    public get demoConfig() : any {
        return {
            isDemo: '____isDemo____',
            demoAdmin: '____demoAdmin____',
            demoAdminPass: '____demoAdminPass____',
            demoUser: '____demoUser____',
            demoUserPass: '____demoUserPass____'
        }
    }

    public get allowWeekendOrders() : string {
        return '____allowWeekendOrders____';
    }
    
    public apiPrefix : string = 'api';
}