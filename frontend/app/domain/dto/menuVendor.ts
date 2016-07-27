import { MenuVendorAddress } from './menuVendorAddress'

export class MenuVendor implements app.domain.dto.IMenuVendor {
    name: string;
    address: MenuVendorAddress;
    website: string;
    submitOrderTime: string;
    logo: string;
}
