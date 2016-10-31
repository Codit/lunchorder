import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { HttpClient } from '../helpers/httpClient';
import { ServiceWorkerDetail } from '../domain/dto/serviceWorkerDetail';
import { ConfigService } from './configService';
import { AccountService } from './accountService';

@Injectable()
export class ServiceworkerService {
    constructor(private http: Http, private httpClient: HttpClient, private configService: ConfigService, private accountService: AccountService) {
        this.serviceWorkerDetail = new ServiceWorkerDetail();
        
        this.accountService.isAuthenticated$.subscribe((isAuthenticated) => {
            if (isAuthenticated && this.serviceWorkerDetail && this.serviceWorkerDetail.endpoint) {
                // update token if different.
                if (this.accountService.user.pushToken !== this.serviceWorkerDetail.endpoint) {
                    this.httpClient.post(`${this.pushesApiUrl}/register?token=${this.serviceWorkerDetail.endpoint}`, null).subscribe();
                }
            }
        });
    }

    serviceWorkerDetail: ServiceWorkerDetail;
    private pushesApiUrl = `${this.configService.apiPrefix}/pushes`;

    init() {
        if ('serviceWorker' in navigator) {
            console.log('Service Worker is supported');
            navigator.serviceWorker.register('service-worker.js').then(function () {
                return navigator.serviceWorker.ready;
            }).then((reg : any) => {
                console.log('Service Worker is ready :^)', reg);

                reg.pushManager.subscribe({
                    userVisibleOnly: true
                }).then((sub : any) => {
                    debugger;
                    this.serviceWorkerDetail = new ServiceWorkerDetail();
                    this.serviceWorkerDetail.endpoint = sub.endpoint;
                    console.log('endpoint:', sub.endpoint);
                });
            }).catch(function (error) {
                console.log('Service Worker error :^(', error);
            });
        }
    }
}