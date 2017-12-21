import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/account.service';
import { BalanceService } from './services/balance.service';
import { ConfigService } from './services/config.service';
import { TokenHelper } from './helpers/tokenHelper';
import { ServiceworkerService } from './services/serviceworker.service';
import { RemindOption } from './domain/dto/remindOption';
import { ReminderService } from './services/reminder.service';
import { Reminder } from './domain/dto/reminder';
import { GetUserInfoResponse } from './domain/dto/getUserInfoResponse'

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

    this.accountService.user$.subscribe(user => {
      this.user = user;
      this.setExistingNotification(user);
    });
  }
  user: GetUserInfoResponse;

  hasNotificationConfigured: boolean;
  selectedNotificationOption: RemindOption;
  setExistingNotification(user: GetUserInfoResponse): void {
    var notificationReminder = this.getReminders(user.reminders);
    if (notificationReminder) {
      this.hasNotificationConfigured = true;
      var remindOption = this.remindOptions.filter(x => x.minutes == notificationReminder.minutes);
      if (remindOption && remindOption.length == 1) {
        this.selectedNotificationOption = remindOption[0];
      }
    }
  }

  getReminders(reminders: Reminder[]) {
    if (reminders) {
      // todo, enum type?
      var match = reminders.filter(x => x.type === 0);
      if (match && match.length == 1) {
        return match[0];
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
  isReminderModal: boolean;
  private uploadsApiUri = `${this.configService.apiPrefix}/uploads`;
  isBusyModal: boolean;
  isServiceworkerEnabled(): boolean {
    return this.serviceworkerService.serviceWorkerDetail.isBrowserEnabled();
  }

  reminderNotificationChange(option: RemindOption) {
    this.selectedNotificationOption = option;
  }

  saveReminder() {
    this.isBusyModal = true;
    this.toggleReminderModal();
    if (this.hasNotificationConfigured) {
      var reminder = new Reminder(0, this.selectedNotificationOption.minutes);
      this.reminderService.saveReminder(reminder).subscribe(() => {
this.isBusyModal = false;
this.hasNotificationConfigured = true;
      }, error => {
        this.isBusyModal = false;
      });
    }
    else {
      this.reminderService.deleteReminder(0).subscribe(() => {
        this.isBusyModal = false;
        this.hasNotificationConfigured = false;
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