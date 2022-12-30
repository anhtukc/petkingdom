import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiServiceOption {
  controllerName = "PetServiceOption/";
  constructor(private http:HttpClient) { }
  public getAllPetServiceOption(petWeight:number){
    return this.http.get<any>(environment.apiUrl +this.controllerName+ `getAll?petWeight=${petWeight}`);
  }
  getOption(petServiceId:string){
    return this.http.get<any>(environment.apiUrl +this.controllerName+ `getByPetServiceId?id=${petServiceId}`)
  }
}
