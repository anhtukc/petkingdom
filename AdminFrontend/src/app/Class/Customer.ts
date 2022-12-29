import { Account } from "./Account";

export class Customer {
    id: string = null!;
    firstName: string = null!;
    lastName: string = null!;
    phonenumber: string = null!;
    address: string = null!;
    email: string = null!;
    file?:File;
    sex?: string;
    image?: string;
    status?: number;
    accountId?: string;
    createdDate?: Date;
    updateDate?: Date;
    account?:Account;
  }