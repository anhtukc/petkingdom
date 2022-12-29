export class Account {
    id?: string ;
    permission?: string ;
    username?: string ;
    password?: string;
    securityStamp?: string;
    concurrencyStamp?: string;
    accessFailedCount?: number;
    lockAccount?: boolean;
    lockTime?: Date;
    phoneConfirmed?: boolean;
    createdDate?: Date;
    updateDate?: Date;
  }