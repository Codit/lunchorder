export class UserBadge implements app.domain.dto.IUserBadge, Serializable<UserBadge> {
    id: string;
    timesEarned: number;

    deserialize(input: any) : UserBadge {
        this.id = input.id;
        this.timesEarned = input.timesEarned;
        return this;
    }
}