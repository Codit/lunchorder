declare module app.domain.dto {
    export interface IAddress {
        street: string;
        streetNumber: string;
        city: string;
        phone: string;
        fax: string;
        email: string;
    }
    
    export interface IBadge {
        id: string;
        name: string;
        icon: string;
        description: string;
    }
    
    export interface ILastOrder {
        id: string;
        userOrderHistoryId: string;
        orderTime: Date;
        finalPrice: number;
    }
    
    export interface ILastOrderEntry {
        name: string;
        appliedRules: string;
        freeText: string;
        price: number;
    }
    
    export interface IMenu {
        id: string;
        enabled: boolean;
        deleted: boolean;
        vendor: IMenuVendor;
        entries: IMenuEntry[];
        categories: IMenuCategory[];
        rules: IMenuRule[];
        lastUpdated: Date;
        revision: number;
    }
    
    export interface IMenuCategory {
        id: string;
        name: string;
        description: string;
        subCategories: IMenuCategory[];
    }
    
    export interface IMenuEntry {
        id: string;
        name: string;
        description: string;
        categoryId: string;
        price: number;
        enabled: boolean;
    }
    
    export interface IMenuEntryFavorite {
        menuEntryId: string;
    }
    
    export interface IMenuOrder {
        menuEntryId: string;
        name: string;
        freeText: string;
        appliedMenuRules: IMenuRule[];
        price: number;
    }
    
    export interface IMenuRule {
        id: string;
        categoryIds: string[];
        description: string;
        priceDelta: number;
        enabled: boolean;
    }
    
    export interface IMenuVendor {
        id: string;
        name: string;
        address: IMenuVendorAddress;
        website: string;
        submitOrderTime: string;
        logo: string;
    }
    
    export interface IMenuVendorAddress extends IAddress {
        
    }
    
    export interface IPlatformUserListItem {
        userId: string;
        userName: string;
        firstName: string;
        lastName: string;
    }
    
    export interface ISimpleUser {
        id: string;
        userName: string;
    }
    
    export interface ITest {
        id: string;
    }
    
    export interface IUserBadge {
        badgeId: string;
        timesEarned: number;
    }
    
    export interface IUserOrderHistory {
        id: string;
        finalPrice: number;
        userName: string;
        userId: string;
        entries: IUserOrderHistoryEntry[];
        orderTime: Date;
    }
    
    export interface IUserOrderHistoryEntry {
        id: string;
        menuEntryId: string;
        name: string;
        freeText: string;
        price: number;
        rules: IUserOrderHistoryRule[];
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
        culture: string;
    }
    
    export interface IVendorHistoryEntryRule {
        id: string;
        description: string;
        priceDelta: number;
    }
    
    export interface IVendorOrderHistory {
        id: string;
        vendorId: string;
        orderDate: string;
        submitted: boolean;
    }
    
    export interface IVendorOrderHistoryEntry {
        id: string;
        name: string;
        menuEntryId: string;
        userId: string;
        userName: string;
        finalPrice: number;
        userOrderHistoryEntryId: string;
        rules: IVendorHistoryEntryRule[];
    }
    
    export interface IPostFavoriteRequest {
        menuEntryId: string;
    }
    
    export interface IPostMenuRequest {
        menu: IMenu;
    }
    
    export interface IPostOrderRequest {
        menuOrders: IMenuOrder[];
    }
    
    export interface IPutBalanceRequest {
        userId: string;
        amount: number;
    }
    
    export interface IPutMenuRequest {
        menu: IMenu;
    }
    
    export interface IGetAllUsersResponse {
        users: IPlatformUserListItem[];
    }
    
    export interface IGetUserInfoResponse {
        id: string;
        userName: string;
        balance: number;
        profile: IUserProfile;
        badges: IUserBadge[];
        favorites: IMenuEntryFavorite[];
        last5Orders: ILastOrder[];
        roles: string[];
        userToken: string;
    }
}