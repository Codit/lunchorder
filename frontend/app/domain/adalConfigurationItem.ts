declare module app.domain {
        export class AdalConfigurationItem {
                tenant: string;
                clientId: string;
                redirectUri: string;
                postLogoutRedirectUri: string;
        }
}