import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/accountService';
import { BalanceService } from './services/balanceService';
import { ConfigService } from './services/configService';
import { TokenHelper } from './helpers/tokenHelper';
import { ServiceworkerService } from './services/serviceworkerService';
import { RemindOption } from './domain/dto/remindOption';
import { ReminderService } from './services/reminderService';
import { Reminder } from './domain/dto/reminder';

@Component({
  selector: '[about-you]',
  templateUrl: 'app.about-you.html'
})

export class AboutYouComponent implements OnInit {

  constructor(private balanceService: BalanceService, private accountService: AccountService, private configService: ConfigService, private tokenHelper: TokenHelper,
    private serviceworkerService: ServiceworkerService, private reminderService: ReminderService) {
    this.remindOptions = new Array<RemindOption>();
    this.remindOptions.push(new RemindOption("15 minutes", 15));
    this.remindOptions.push(new RemindOption("30 minutes", 30));
    this.remindOptions.push(new RemindOption("1 hour", 60));
    this.remindOptions.push(new RemindOption("1 hour 30 minutes", 90));
    this.remindOptions.push(new RemindOption("2 hours", 120));

    this.setExistingNotification();
  }

  hasNotificationConfigured: boolean;
  selectedNotificationOption: RemindOption;
  setExistingNotification(): void {
    var notificationReminder = this.accountService.user.getNotificationReminder();
    if (notificationReminder) {
      this.hasNotificationConfigured = true;
      var remindOption = this.remindOptions.filter(x => x.minutes == notificationReminder.minutes);
      if (remindOption && remindOption.length == 1) {
        this.selectedNotificationOption = remindOption[0];
      }
    }
  }

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

  remindOptions: Array<RemindOption>;
  private isReminderModal: boolean;
  private uploadsApiUri = `${this.configService.apiPrefix}/uploads`;
  private isBusyModal: boolean;
  isServiceworkerEnabled(): boolean {
    return this.serviceworkerService.serviceWorkerDetail.isBrowserEnabled();
  }

  save() {
    this.isBusyModal = true;
    this.toggleReminderModal();
    if (this.hasNotificationConfigured) {
      var reminder = new Reminder(0, this.selectedNotificationOption.minutes);
      this.reminderService.saveReminder(reminder).subscribe(() => {

        // set in user object.

      });
    }
    else {
      this.reminderService.deleteReminder(0).subscribe(() => {

        // set in user object.

      });;
     }
  }

  toggleReminderModal() {
    this.isReminderModal = !this.isReminderModal;
  }
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
      this.accountService.user.profile.picture = data;
    }
  }

  getName(): string {
    if (this.accountService.user.profile && this.accountService.user.profile.firstName && this.accountService.user.profile.lastName)
      return `${this.accountService.user.profile.firstName} ${this.accountService.user.profile.lastName}`
    return this.accountService.user.userName;
  }
}