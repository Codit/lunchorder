export class MenuRule implements app.domain.dto.IMenuRule {
    id: string;
    categoryIds: number[];
    description: string;
    priceDelta: number;
    enabled: boolean;
}