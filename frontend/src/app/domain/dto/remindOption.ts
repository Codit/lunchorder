export class RemindOption {
    text: string;
    minutes: number;

    constructor(text: string, minutes:number) {
        this.text = text;
        this.minutes = minutes;
    }
}