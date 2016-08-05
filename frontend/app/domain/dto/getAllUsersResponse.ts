import { PlatformUserListItem } from './platformUserListItem';

export class GetAllUsersResponse implements app.domain.dto.IGetAllUsersResponse, Serializable<GetAllUsersResponse> {
     users: PlatformUserListItem[];

     deserialize(input : any) : GetAllUsersResponse {
        this.users = new Array<PlatformUserListItem>();
        for (var user of input.users) {
            this.users.push(new PlatformUserListItem().deserialize(user));
        }
        
        return this;
    }
}