import { pagination } from "./pagination";

export interface basedSearchObject{
    name?:string;
    status?:number;
    phoneNumber?:string;
    startDate?:string;
    endDate?:string;
}

export interface postingObjectWithId{
    page:pagination;
    searchObj:basedSearchObject;
    id:string;
}