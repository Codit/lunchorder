export class MenuEntryFavorite implements app.domain.dto.IMenuEntryFavorite, Serializable<MenuEntryFavorite> {
    menuEntryId: string;

    deserialize(input : any) : MenuEntryFavorite {
        this.menuEntryId = input.menuEntryId;
        return this;
    }
}