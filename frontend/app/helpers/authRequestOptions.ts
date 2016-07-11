import { Headers, BaseRequestOptions, RequestOptions } from '@angular/http';
import { AdalService } from 'angular2-adal/core';

export class AuthRequestOptions extends BaseRequestOptions {
    constructor(private adalService: AdalService) {
        super();
        // todo add token using acquiretoken from adal when implemented.
        this.headers.append('Authorization','Bearer TODOADDTOKEN');
    }
}