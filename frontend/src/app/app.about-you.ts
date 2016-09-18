import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/accountService';
import { BalanceService } from './services/balanceService';
import { ConfigService } from './services/configService';
import { UPLOAD_DIRECTIVES } from 'ng2-uploader/ng2-uploader';
import { TokenHelper } from './helpers/tokenHelper';

@Component({
  selector: '[about-you]',
  templateUrl: 'app.about-you.html',
  directives: [UPLOAD_DIRECTIVES]
})

export class AboutYouComponent implements OnInit {

  constructor(private balanceService: BalanceService, private accountService: AccountService, private configService: ConfigService, private tokenHelper: TokenHelper) { }

  ngOnInit() {
    this.accountService.isAuthenticated$.subscribe((isAuthenticated) => {
      if (isAuthenticated) {
        // this.options['authToken'] = this.tokenHelper.authToken;
        this.options  = {
          url: this.uploadsApiUri,
          filterExtensions: true,
          allowedExtensions: ['jpg', 'png'],
          authToken: this.tokenHelper.authToken,
        };
      }
    });
  }

  private uploadsApiUri = `${this.configService.apiPrefix}/uploads`;

  uploadFile: any;
  options: Object  = {
    url: this.uploadsApiUri,
    filterExtensions: true,
    allowedExtensions: ['jpg', 'png', 'jpeg'],
    authToken: this.tokenHelper.authToken,
  };

  handleUpload(data: any): void {
    if (data && data.response) {
      data = JSON.parse(data.response);
      this.uploadFile = data;
      this.accountService.user.profile.picture = data;
    }
  }

  getName(): string {
    if (this.accountService.user.profile && this.accountService.user.profile.firstName && this.accountService.user.profile.lastName)
      return `${this.accountService.user.profile.firstName} ${this.accountService.user.profile.lastName}`
    return this.accountService.user.userName;
  }
}