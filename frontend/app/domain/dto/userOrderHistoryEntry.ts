import { UserOrderHistoryRule } from './userOrderHistoryRule';

export class UserOrderHistoryEntry implements app.domain.dto.IUserOrderHistoryEntry {
    id: string;
    name: string;
    freeText: string;
    price: number;
    menuEntryId: string;
    rules: UserOrderHistoryRule[];
}