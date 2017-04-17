export class MenuVendorClosingDateRange implements app.domain.dto.IMenuVendorClosingDateRange, Serializable<MenuVendorClosingDateRange> {
    from: string;
    until: string;

    deserialize(input : any) : MenuVendorClosingDateRange {
        if (!input) { return; }
        this.from = input.from;
        this.until = input.until;
        
        return this;
    }
}
