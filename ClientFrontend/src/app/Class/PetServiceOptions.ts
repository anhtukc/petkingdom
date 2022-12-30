import { ScheduleAvailable } from "./ScheduleAvailable";
import { ServiceSellPrice } from "./ServiceSellPrice";

export class ServiceOption {
    id: string = '';
    petServiceId: string = '';
    name: string = '';
    description?: string;
    estimatedCompletionTime?: number;
    status?: number;
    createdDate?: Date;
    updateDate?: Date;
    petServiceName?: string ;
    price?:number;
  }