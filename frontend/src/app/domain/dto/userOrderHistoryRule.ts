export class UserOrderHistoryRule implements app.domain.dto.IUserOrderHistoryRule {
    id: string;
    description: string;
    priceDelta: number;
}