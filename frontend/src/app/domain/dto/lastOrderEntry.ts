export class LastOrderEntry implements app.domain.dto.ILastOrderEntry, Serializable<LastOrderEntry> {
        name: string;
        appliedRules: string;
        freeText: string;
        price: number;

        deserialize(input: any) : LastOrderEntry {
                if (!input) return;
                this.name = input.name;
                this.appliedRules = input.appliedRules;
                this.freeText = input.freeText;
                this.price = input.price;
                return this;
        }
}