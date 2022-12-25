import { pagination } from "./pagination";

export interface basedSearchObject{
    name?:string;
    status?:number;
}

export interface postingObjectWithId{
    page:pagination;
    searchObj:basedSearchObject;
    id:string;
}