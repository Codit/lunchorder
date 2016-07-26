export class MenuRule implements app.domain.dto.IMenuRule {
    id: string;
    categoryIds: string[];
    description: string;
    priceDelta: number;
    enabled: boolean;
}