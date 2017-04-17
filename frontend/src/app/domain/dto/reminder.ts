export class Reminder implements app.domain.dto.IReminder {
    constructor (type: number, minutes: number) {
        this.type = type;
        this.minutes = minutes;
    }

    // todo, export enum from csharp?
    type: number;
    minutes: number;
}