export class Badge implements app.domain.dto.IBadge {
    id: string;
    name: string;
    icon: string;
    description: string;
    earned: boolean;
}