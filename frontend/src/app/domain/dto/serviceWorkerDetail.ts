export class ServiceWorkerDetail {
    endpoint: string;
    isBrowserEnabled() : boolean {
        if (this.endpoint) return true;
    }
}