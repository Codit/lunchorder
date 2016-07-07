declare module api.dto {
    export interface IAddress {
        street: string;
        streetNumber: string;
        city: string;
        phone: string;
        email: string;
    }
    
    export interface IBuyer {
        id: string;
        name: string;
        address: IBuyerAddress;
        valuta: string;
    }
    
    export interface IBuyerAddress extends IAddress {
        id: string;
        enabled: boolean;
        deleted: boolean;
        vendor: IMenuVendor;
        entries: IMenuEntry[];
        categories: IMenuCategory[];
        rules: IMenuRule[];
    }
    
    export interface IMenuCategory {
        id: string;
        name: string;
        description: string;
    }
    
    export interface IMenuEntry {
        id: string;
        name: string;
        description: string;
        categoryId: string;
        picture: string;
        price: string;
        enabled: boolean;
    }
    
    export interface IMenuRule {
        id: string;
        categoryIds: number[];
        description: string;
        priceDelta: number;
        enabled: boolean;
    }
    
    export interface IMenuVendor {
        address: IMenuVendorAddress;
        submitOrderTime: ITimeSpan;
    }
    
    export interface IMenuVendorAddress extends IAddress {
        id: string;
        userId: string;
        orderHistories: IUserOrderHistory[];
    }
    
    export interface IUserOrderHistory {
        id: string;
        finalPrice: number;
        entry: IUserOrderHistoryEntry;
        orderTime: Date;
        rules: IUserOrderHistoryRule[];
    }
    
    export interface IUserOrderHistoryEntry {
        id: string;
        name: string;
        freeText: string;
        price: number;
    }
    
    export interface IUserOrderHistoryRule {
        id: string;
        description: string;
        priceDelta: number;
    }
    
    export interface IVendorHistoryEntry {
        id: string;
        name: string;
        userId: string;
        userName: string;
        finalPrice: number;
        userOrderHistoryId: string;
        rules: IVendorHistoryEntryRule[];
    }
    
    export interface IVendorHistoryEntryRule {
        id: string;
        description: string;
        priceDelta: number;
    }
    
    export interface IVendorOrderHistory {
        id: string;
        vendorId: string;
        orderTime: Date;
        entries: IVendorHistoryEntry[];
        submitted: boolean;
    }
}