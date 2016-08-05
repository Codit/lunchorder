export class LastOrder implements app.domain.dto.ILastOrder, Serializable<LastOrder> {
        id: string;
        userOrderHistoryId: string;
        orderTime: Date;
        finalPrice: number;

        deserialize(input: any) : LastOrder {
                this.id = input.id;
                this.userOrderHistoryId = input.userOrderHistoryId;
                this.orderTime = input.orderTime;
                this.finalPrice = input.finalPrice;
                return this;
        }
}