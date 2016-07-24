import { UserProfile } from './userProfile'
import { UserBadge } from './userBadge'
import { MenuEntryFavorite } from './menuEntryFavorite'

export class GetUserInfoResponse implements app.domain.dto.IGetUserInfoResponse {
    id: string;
    userName: string;
    balance: number;
    profile: UserProfile;
    badges: UserBadge[];
    favorites: MenuEntryFavorite[];
    userToken: string;
}