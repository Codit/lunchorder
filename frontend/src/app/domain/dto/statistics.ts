export class Statistics implements app.domain.dto.IStatistics, Serializable<Statistics> {
    appTotalSpend: number;
    weeklyTotalAmount: number;
    monthlyTotalAmount: number;
    weeklyHealthyOrders: number;
    yearlyTotalAmount: number;
    yearlyPastas: number;
    prepayedTotal: number;

    deserialize(input: any): Statistics {
        this.appTotalSpend = input.appTotalSpend;
        this.weeklyTotalAmount = input.weeklyTotalAmount;
        this.monthlyTotalAmount = input.monthlyTotalAmount;
        this.weeklyHealthyOrders = input.weeklyHealthyOrders;
        this.yearlyTotalAmount = input.yearlyTotalAmount;
        this.yearlyPastas = input.yearlyPastas;
        this.prepayedTotal = input.prepayedTotal;
        return this;
    }
}