export class UserProfile implements app.domain.dto.IUserProfile, Serializable<UserProfile> {
    firstName: string;
    lastName: string;
    picture: string;
    culture: string;

    deserialize(input: any) : UserProfile {
        this.firstName = input.firstName;
        this.lastName = input.lastName;
        this.picture = input.picture;
        this.culture = input.culture;
        return this;
    }
}