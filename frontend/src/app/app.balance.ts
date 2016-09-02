import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/accountService';
import { BalanceService } from './services/balanceService';
import { ConfigService } from './services/configService';
import { UPLOAD_DIRECTIVES } from 'ng2-uploader/ng2-uploader';
import { TokenHelper } from './helpers/tokenHelper';

@Component({
	selector: '[balance]',
	templateUrl: 'app.balance.html',
	directives: [UPLOAD_DIRECTIVES]})

export class BalanceComponent implements OnInit {

	constructor(private balanceService: BalanceService, private accountService: AccountService, private configService: ConfigService, private tokenHelper: TokenHelper) { }

	ngOnInit() {
		
	}

private uploadsApiUri = `${this.configService.apiPrefix}/uploads`;

	 uploadFile: any;
  options: Object = {
    url: this.uploadsApiUri,
	authToken: this.tokenHelper.authToken,
  };

  handleUpload(data : any): void {
    if (data && data.response) {
      data = JSON.parse(data.response);
      this.uploadFile = data;
    }
  }

    getName(): string {
        if (this.accountService.user.profile && this.accountService.user.profile.firstName && this.accountService.user.profile.lastName)
            return `${this.accountService.user.profile.firstName} ${this.accountService.user.profile.lastName}`
        return this.accountService.user.userName;
    }
}