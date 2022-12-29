import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiPetServiceImage {
  controllerName ="PetServiceImage";
  constructor(private http:HttpClient) { }
  public getById(id:string){
    return this.http.get<any>(environment.apiUrl +this.controllerName+ `/getall?serviceId=${id}`);
  }
}
