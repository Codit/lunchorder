import { MenuVendor } from './MenuVendor'
import { MenuEntry } from './MenuEntry'
import { MenuCategory } from './MenuCategory'
import { MenuRule } from './MenuRule'

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
}