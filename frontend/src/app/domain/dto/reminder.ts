export class Reminder implements app.domain.dto.IReminder {
    // todo, export enum from csharp?
    type: string;
    minutes: number;
}