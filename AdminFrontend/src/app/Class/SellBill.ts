import { Account } from "./Account";
import { Customer } from "./Customer";
import { Employee } from "./Employee";

export class SellBill {
    id: string = null!;
    createdDate?: Date;
    totalPaid?: number;
    customerAccountId?: string;
    employeeAccountId?: string;
    status?: number;
    billType?: string;
    updateDate?: Date;
    customer?:Customer;
    employee?:Employee;
    Employee?:Employee;
    Customer?:Customer;
  }