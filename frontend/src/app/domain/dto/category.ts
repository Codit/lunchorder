export class MenuCategory implements app.domain.dto.IMenuCategory {
        id: string;
        name: string;
        description: string;
        subCategories: MenuCategory[];
}