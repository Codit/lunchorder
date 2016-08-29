import { LastOrderEntry } from './lastOrderEntry'

export class LastOrder implements app.domain.dto.ILastOrder, Serializable<LastOrder> {
        id: string;
        userOrderHistoryId: string;
        orderTime: Date;
        finalPrice: number;
        lastOrderEntries: LastOrderEntry[];

        deserialize(input: any) : LastOrder {
                this.id = input.id;
                this.userOrderHistoryId = input.userOrderHistoryId;
                this.orderTime = input.orderTime;
                this.finalPrice = input.finalPrice;
                this.lastOrderEntries = new Array<LastOrderEntry>();
                if (input.lastOrderEntries) {
                        for (var entry of input.lastOrderEntries) {
                                this.lastOrderEntries.push(new LastOrderEntry().deserialize(entry));
                        }
                }
                return this;
        }
}