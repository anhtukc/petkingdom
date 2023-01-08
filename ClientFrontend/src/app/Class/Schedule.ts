export interface Schedule {
    id: string ;
    serviceOptionName: string ;
    bookingDate: Date;
    bookingHour: Date;
    grandPaid?: number;
    status?: number;
    serviceOptionId?: string;
    sellBillId?: string;
    petId?: string;
    createdDate?: Date;
    updateDate?: Date;
    selectedTableId?:string
  }
