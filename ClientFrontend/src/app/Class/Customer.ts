import { Account } from "./Account";

export class Customer {
    id?: string;
    firstName?: string ;
    lastName?: string ;
    phonenumber?: string ;
    address?: string ;
    email?: string;
    file?:File;
    sex?: string;
    image?: string;
    status?: number;
    accountId?: string;
    createdDate?: Date;
    updateDate?: Date;
    account?:Account;
  }