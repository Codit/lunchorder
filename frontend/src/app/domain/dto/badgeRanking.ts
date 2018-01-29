export class BadgeRanking implements app.domain.dto.IBadgeRanking, Serializable<BadgeRanking> {
    userId: string;
    userName: string;
    picture: string;
    totalBadges: string;

    deserialize(input: any): BadgeRanking {
        this.userId = input.userId;
        this.userName = input.userName;
        this.picture = input.picture;
        this.totalBadges = input.totalBadges;
        return this;
    }
}