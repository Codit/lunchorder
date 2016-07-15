import { Injectable } from '@angular/core';
import { ConfigService } from '../services/configService';

@Injectable()
export class TokenHelper {
    constructor(private configService: ConfigService) {
    }

    getToken() {
        if(window.location.href.indexOf('id_token') > -1) {
				var url = window.location.href;
				var params = url.split('#')[1].split('&') 
				for(var i =0;i<params.length;i++){
					var temp = params[i].split('=');
					var key   = temp[0];
					var value = temp[1];
					if(key.indexOf('id_token') > -1) {
						this.configService.authToken = value;
					}
				}
			}
    }
}