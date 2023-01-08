export class SellBill {
    id: string = null!;
    createdDate?: Date;
    totalPaid?: number;
    customerAccountId?: string;
    employeeAccountId?: string;
    status?: number;
    billType?: string;
    updateDate?: Date;
  }