import { UserOrderHistoryEntry } from './userOrderHistoryEntry'
import { UserOrderHistoryRule } from './userOrderHistoryRule'

export class MenuVendorAddress implements app.domain.dto.IMenuVendorAddress, Serializable<MenuVendorAddress> {
    street: string;
    streetNumber: string;
    city: string;
    phone: string;
    email: string;
    fax: string;

    deserialize(input: any) : MenuVendorAddress {
        this.street = input.street;
        this.streetNumber = input.streetNumber;
        this.city = input.city;
        this.phone = input.phone;
        this.email = input.email;
        this.fax = input.fax;
        return this;
    }
}
