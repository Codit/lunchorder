export class UserBadge implements app.domain.dto.IUserBadge, Serializable<UserBadge> {
    badgeId: string;
    timesEarned: number;

    deserialize(input: any) : UserBadge {
        this.badgeId = input.badgeId;
        this.timesEarned = input.timesEarned;
        return this;
    }
}