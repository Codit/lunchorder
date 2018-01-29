import { Badge } from './badge';
import { BadgeRanking } from './badgeRanking';

export class GetBadgesResponse implements app.domain.dto.IGetBadgesResponse, Serializable<GetBadgesResponse> {
    constructor() { }
    badgeRankings: BadgeRanking[];
    badges: Badge[];

    deserialize(input: any): GetBadgesResponse {
        this.badges = new Array<Badge>();
        this.badgeRankings = new Array<BadgeRanking>();

        if (input.badges) {
            for (var badge of input.badges) {
                this.badges.push(new Badge().deserialize(badge));
            }
        }

        if (input.badgeRankings) {
            for (var ranking of input.badgeRankings) {
                this.badgeRankings.push(new BadgeRanking().deserialize(ranking));
            }
        }
       
        return this;
    }
}