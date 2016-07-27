import { MenuVendor } from './menuVendor'
import { MenuEntry } from './menuEntry'
import { MenuCategory } from './menuCategory'
import { MenuRule } from './menuRule'

export class Menu implements app.domain.dto.IMenu {
        constructor(obj : any) {
                this.id = obj.id || {};
        }

        id: string;
        enabled: boolean;
        deleted: boolean;
        vendor: MenuVendor;
        entries: MenuEntry[];
        categories: MenuCategory[];
        rules: MenuRule[];
        lastUpdated: Date;
        revision: number;
}