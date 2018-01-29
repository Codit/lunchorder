import { MenuRule } from './menuRule'

export class MenuOrder implements app.domain.dto.IMenuOrder {
        id: number;
        menuEntryId: string;
        freeText: string;
        appliedMenuRules: MenuRule[];
        price: number;
        name: string;
        healthy: boolean;
        pasta: boolean;
}