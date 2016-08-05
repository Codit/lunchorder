import { UserProfile } from './userProfile';
import { UserBadge } from './userBadge';
import { MenuEntryFavorite } from './menuEntryFavorite';
import { LastOrder } from './lastOrder';

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
        this.roles = input.roles;

        return this;
    }
}