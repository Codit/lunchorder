declare module api.dto {
    export interface IAddress {
        street: string;
        streetNumber: string;
        city: string;
        phone: string;
        email: string;
    }
    
    export interface IBadge {
        name: string;
        icon: string;
        description: string;
        earned: boolean;
    }
    
    export interface IMenu {
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
    
    export interface IMenuEntryFavorite {
        menuEntryId: string;
    }
    
    export interface IMenuOrder {
        menuEntryId: string;
        freeText: string;
        appliedMenuRules: IMenuRule[];
        price: number;
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
        submitOrderTime: string;
    }
    
    export interface IMenuVendorAddress extends IAddress {
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
    
    export interface IUserProfile {
        firstName: string;
        lastName: string;
        picture: string;
    }
    
    export interface IPostFavoriteRequest {
        menuEntryId: string;
    }
    
    export interface IPostMenuRequest {
        menu: IMenu;
    }
    
    export interface IPostUserHistoryRequest {
        menuOrders: IMenuOrder[];
    }
    
    export interface IPutMenuRequest {
        menu: IMenu;
    }
    
    export interface IGetUserInfoResponse {
        balance: number;
        profile: IUserProfile;
        badges: IBadge[];
        favorites: IMenuEntryFavorite[];
    }
}