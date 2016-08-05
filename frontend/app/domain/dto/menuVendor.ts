import { MenuVendorAddress } from './menuVendorAddress'

export class MenuVendor implements app.domain.dto.IMenuVendor, Serializable<MenuVendor> {
    id: string;
    name: string;
    address: MenuVendorAddress;
    website: string;
    submitOrderTime: string;
    logo: string;

    deserialize(input : any) : MenuVendor {
        this.id = input.id;
        this.name = input.name;
        this.address = new MenuVendorAddress().deserialize(input.address);
        this.website = input.website;
        this.submitOrderTime = input.submitOrderTime;
        this.logo = input.logo;
        
        return this;
    }
}
