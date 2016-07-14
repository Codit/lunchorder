import { MenuVendorAddress } from './menuVendorAddress'

export class MenuVendor implements app.domain.dto.IMenuVendor {
    name: string;
    address: MenuVendorAddress;
    submitOrderTime: string;
}
