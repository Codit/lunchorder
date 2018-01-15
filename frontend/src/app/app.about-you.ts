import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/account.service';
import { BalanceService } from './services/balance.service';
import { ConfigService } from './services/config.service';
import { TokenHelper } from './helpers/tokenHelper';
import { GetUserInfoResponse } from './domain/dto/getUserInfoResponse'

@Component({
  selector: '[about-you]',
  templateUrl: 'app.about-you.html'
})

export class AboutYouComponent implements OnInit {

  constructor(private balanceService: BalanceService, private accountService: AccountService, private configService: ConfigService, private tokenHelper: TokenHelper) {
    this.accountService.user$.subscribe(user => {
      this.user = user;
    });
  }
  user: GetUserInfoResponse;

  ngOnInit() {
    this.accountService.isAuthenticated$.subscribe((isAuthenticated) => {
      if (isAuthenticated) {
        // this.options['authToken'] = this.tokenHelper.authToken;
        this.options = {
          url: this.uploadsApiUri,
          filterExtensions: true,
          allowedExtensions: ['jpg', 'png'],
          authToken: this.tokenHelper.authToken,
        };
      }
    });
  }

  private uploadsApiUri = `${this.configService.apiPrefix}/uploads`;
  isBusyModal: boolean;

  uploadFile: any;
  options: Object = {
    url: this.uploadsApiUri,
    filterExtensions: true,
    allowedExtensions: ['jpg', 'png', 'jpeg'],
    authToken: this.tokenHelper.authToken,
  };

  handleUpload(data: any): void {
    if (data && data.response) {
      data = JSON.parse(data.response);
      this.uploadFile = data;
      this.user.profile.picture = data;
    }
  }

  getName(): string {
    if (this.user) {
      if (this.user.profile && this.user.profile.firstName && this.user.profile.lastName) {
        return `${this.user.profile.firstName} ${this.user.profile.lastName}`
      }
      return this.user.userName;
    }
  }
}