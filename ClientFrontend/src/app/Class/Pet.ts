export class Pet {
    id: string = null!;
    name: string = null!;
    weight?: number;
    birthday?: Date;
    image?: string;
    file?:File;
    birthDayFormat?:string
    specices?: string;
    breed?: string;
    sex: string = null!;
    healthNote?: string;
    status?: number;
    customerId?: string;
    createdDate?: Date;
    updateDate?: Date;
  }