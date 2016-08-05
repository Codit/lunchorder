export class MenuRule implements app.domain.dto.IMenuRule, Serializable<MenuRule> {
    id: string;
    categoryIds: string[];
    description: string;
    priceDelta: number;
    enabled: boolean;
    isEffective: boolean;
    isSelected: boolean;

    deserialize(input: any) : MenuRule {
        this.id = input.id;
        
        this.categoryIds = new Array<string>();
        for (var categoryId in input.categoryIds) {
            this.categoryIds.push(categoryId);
        }
        this.description = input.description;
        this.priceDelta = input.priceDelta;
        this.enabled = input.enabled;
        this.isEffective = input.isEffective;
        this.isSelected = input.isSelected;

        return this;
        }
}