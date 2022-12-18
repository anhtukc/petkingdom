import { Guid } from "guid-typescript";
export interface petService{
    id:string;
    name:string;
    fullDesciption:string;
    briefDescription:string;
    icon?:string;
    status: number;
    iconlFile?:File;
}

