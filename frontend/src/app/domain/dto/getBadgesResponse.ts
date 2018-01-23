import { Badge } from './badge';

export class GetBadgesResponse implements app.domain.dto.IGetBadgesResponse, Serializable<GetBadgesResponse> {
    constructor() { }

    badges: Badge[];

    deserialize(input: any): GetBadgesResponse {
        this.badges = new Array<Badge>();

        if (input.badges) {
            for (var badge of input.badges) {
                this.badges.push(new Badge().deserialize(badge));
            }
        }
       
        return this;
    }
}