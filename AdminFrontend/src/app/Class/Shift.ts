import { Pet } from "./Pet";

export class Shift {
    id?: string;
    workingDate: Date;
    startedHour: Date;
    endedHour: Date;
    scheduleId?: string;
    caringStaffId?: string;
    status?: number;
    createdDate?: Date;
    updateDate?: Date;
    WorkingDateFormat?:string
    pet?:Pet;
  }
  