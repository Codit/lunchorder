export class UserBalanceAuditItem implements app.domain.dto.IUserBalanceAuditItem, Serializable<UserBalanceAuditItem> {
        date: Date;
        originatorName: string;
        amount: number;

        deserialize(input: any): UserBalanceAuditItem {
                if (!input) return;

                this.date = input.date;
                this.originatorName = input.originatorName;
                this.amount = this.amount;
                return this;
        }
}