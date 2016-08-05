import { MenuRule } from './menuRule'

export class MenuEntry implements app.domain.dto.IMenuEntry, Serializable<MenuEntry> {
    id: string;
    name: string;
    description: string;
    categoryId: string;
    picture: string;
    price: number;
    enabled: boolean;
    rules: MenuRule[];

    deserialize(input: any): MenuEntry {
        this.id = input.id;
        this.name = input.name;
        this.description = input.description;
        this.categoryId = input.categoryId;
        this.picture = input.picture;
        this.price = input.price;
        this.enabled = input.enabled;

        this.rules = new Array<MenuRule>();
        if (input.rules) {
            for (var rule of input.rules) {
                this.rules.push(new MenuRule().deserialize(rule));
            }
        }
        return this;
    }
}