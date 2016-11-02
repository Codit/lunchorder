import { UserProfile } from './userProfile';
import { UserBadge } from './userBadge';
import { MenuEntryFavorite } from './menuEntryFavorite';
import { LastOrder } from './lastOrder';
import { UserBalanceAuditItem } from './userBalanceAuditItem';
import { Reminder } from './reminder';

export class GetUserInfoResponse implements app.domain.dto.IGetUserInfoResponse, Serializable<GetUserInfoResponse> {
    constructor() { }

    id: string;
    userName: string;
    balance: number;
    profile: UserProfile;
    badges: UserBadge[];
    favorites: MenuEntryFavorite[];
    userToken: string;
    last5Orders: LastOrder[];
    roles: string[];
    pushToken: string;
    last5BalanceAuditItems: UserBalanceAuditItem[];
    reminders: Reminder[];

    getNotificationReminder() : Reminder {
        if (this.reminders) {
            // todo, enum type?
            var match = this.reminders.filter(x => x.type === 0);
            if (match && match.length == 1) {
                return match[0];
            }
        }
    }

    deserialize(input: any): GetUserInfoResponse {
        this.id = input.id;
        this.userName = input.userName;
        this.balance = input.balance;
        this.profile = new UserProfile().deserialize(input.profile);
        this.badges = new Array<UserBadge>();

        if (input.badges) {
            for (var badge of input.badges) {
                this.badges.push(new UserBadge().deserialize(badge));
            }
        }
        this.favorites = new Array<MenuEntryFavorite>();
        if (input.favorites) {
            for (var favorite of input.favorites) {
                this.favorites.push(new MenuEntryFavorite().deserialize(favorite));
            }
        }
        this.userToken = input.userToken;
        this.last5Orders = new Array<LastOrder>();
        if (input.last5Orders) {
            for (var last5Order of input.last5Orders) {
                this.last5Orders.push(new LastOrder().deserialize(last5Order));
            }
        }

        this.last5BalanceAuditItems = new Array<UserBalanceAuditItem>();
        if (input.last5BalanceAuditItems) {
            for (var balanceAudit of input.last5BalanceAuditItems) {
                this.last5BalanceAuditItems.push(new UserBalanceAuditItem().deserialize(balanceAudit));
            }
        }

        this.roles = input.roles;
        this.pushToken = input.pushToken;

        return this;
    }
}