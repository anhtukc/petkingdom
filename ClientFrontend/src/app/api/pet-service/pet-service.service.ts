import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiPetService {
  controllerName:string = "PetService";
  constructor(private http:HttpClient) { }
  public getAllPetService(){
    return this.http.get<any>(environment.apiUrl +this.controllerName+ "/getAll");
}
public getById(id:string){
  return this.http.get<any>(environment.apiUrl +this.controllerName+ `/getById?id=${id}`);
}
}
