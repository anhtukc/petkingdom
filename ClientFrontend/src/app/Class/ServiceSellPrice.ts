export interface ServiceSellPrice {
    id: string;
    unitPrice?: number;
    petMinimumWeight?: number;
    petMaximumWeight?: number;
    status?: number;
    serviceOptionId?: string;
    createdDate?: Date;
    updateDate?: Date;   
  }