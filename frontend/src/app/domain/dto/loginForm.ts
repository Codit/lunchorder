export class LoginForm {
        userName: string;
        password: string;

        createPayload() : string {
            return `username=${this.userName}&password=${this.password}&grant_type=password`
        }
}