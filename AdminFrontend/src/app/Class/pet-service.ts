import { Guid } from "guid-typescript";
export interface petService{
    id:Guid;
    name:String;
    fullDesciption:String;
    briefDescription:String;
    thumbnail:String;
    icon:String;
    status: number;
    thumbnailFile?:File;
    iconlFile?:File;
}

