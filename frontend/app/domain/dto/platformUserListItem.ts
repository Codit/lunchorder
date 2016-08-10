export class PlatformUserListItem implements app.domain.dto.IPlatformUserListItem, Serializable<PlatformUserListItem> {
        userId: string;
        userName: string;
        firstName: string;
        lastName: string;

    getName() {
        if (!this.firstName || !this.lastName)
            return this.userName;
        return `{this.firstName} {this.lastName}`
    }

     deserialize(input : any) : PlatformUserListItem {
        this.userId = input.userId;
        this.userName = input.userName;
        this.firstName = input.firstName;
        this.lastName = input.lastName;
        
        return this;
    }
}