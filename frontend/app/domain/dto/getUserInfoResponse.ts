import { UserProfile } from './userProfile'
import { Badge } from './badge'
import { MenuEntryFavorite } from './menuEntryFavorite'

export class GetUserInfoResponse implements app.domain.dto.IGetUserInfoResponse {
    balance: number;
    profile: UserProfile;
    badges: Badge[];
    favorites: MenuEntryFavorite[];
}