import { MenuVendor } from './menuVendor'
import { MenuEntry } from './menuEntry'
import { MenuCategory } from './menuCategory'
import { MenuRule } from './menuRule'

export class Menu implements app.domain.dto.IMenu, Serializable<Menu> {
        id: string;
        enabled: boolean;
        deleted: boolean;
        vendor: MenuVendor;
        entries: MenuEntry[];
        categories: MenuCategory[];
        rules: MenuRule[];
        lastUpdated: Date;
        revision: number;

        deserialize(input: any): Menu {
                if (!input) return;
                
                this.id = input.id;
                this.enabled = input.enabled;
                this.deleted = input.deleted;
                this.vendor = new MenuVendor().deserialize(input);

                this.entries = new Array<MenuEntry>();
                for (var entry in input.entries) {
                        this.entries.push(new MenuEntry().deserialize(entry));
                }

                this.categories = new Array<MenuCategory>();
                for (var category in input.categories) {
                        this.categories.push(new MenuCategory().deserialize(category));
                }

                this.rules = new Array<MenuRule>();
                for (var rule in input.rules) {
                        this.rules.push(new MenuRule().deserialize(rule));
                }

                for (let menuRule of this.rules) {
                        var categories = this.categories.filter((category) => menuRule.categoryIds.find((ruleCategory) => ruleCategory == category.id) != null);
                        for (let category of categories) {
                                var menuEntries = this.entries.filter((menuEntry) => menuEntry.categoryId == category.id);
                                for (let menuEntry of menuEntries) {
                                        // todo this should be added to constructor on mapping
                                        if (!menuEntry.rules) { menuEntry.rules = new Array<MenuRule>(); }
                                        menuEntry.rules.push(menuRule);
                                }

                                this.recurseSubCategory(this, menuRule, category);
                        }
                }

                this.lastUpdated = input.lastUpdated;
                this.revision = input.revision;
                return this;
        }

        recurseSubCategory = (menu: Menu, menuRule: MenuRule, category: MenuCategory): void => {
                if (category.subCategories) {
                        for (let subCategory of category.subCategories) {
                                var menuEntries = menu.entries.filter((menuEntry) => menuEntry.categoryId == subCategory.id);
                                for (let menuEntry of menuEntries) {
                                        // todo this should be added to constructor on mapping
                                        if (!menuEntry.rules) { menuEntry.rules = new Array<MenuRule>(); }
                                        menuEntry.rules.push(menuRule);
                                }
                                this.recurseSubCategory(menu, menuRule, subCategory)
                        }
                }
        }
}