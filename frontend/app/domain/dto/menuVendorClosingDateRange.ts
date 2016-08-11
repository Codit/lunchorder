export class MenuVendorClosingDateRange implements app.domain.dto.IMenuVendorClosingDateRange, Serializable<MenuVendorClosingDateRange> {
    from: string;
    untill: string;

    deserialize(input : any) : MenuVendorClosingDateRange {
        if (!input) { return; }
        this.from = input.from;
        this.untill = input.untill;
        
        return this;
    }
}
