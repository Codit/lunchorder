import { Reminder } from './reminder';

export class PostReminderRequest implements app.domain.dto.IPostReminderRequest, Serializable<PostReminderRequest> {
    constructor() { }

    reminder: Reminder;


    deserialize(input: any): PostReminderRequest {
        this.reminder = input.reminder;

        return this;
    }
}