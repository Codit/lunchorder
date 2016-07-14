export class MenuEntry implements app.domain.dto.IMenuEntry {
    id: string;
    name: string;
    description: string;
    categoryId: string;
    picture: string;
    price: string;
    enabled: boolean;
}