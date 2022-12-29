import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiServiceOption {
  constructor(private http:HttpClient) { }

  getOption(petServiceId:string){
    return this.http.get<any>(environment.apiUrl +"PetServiceOption"+ `/getByPetServiceId?id=${petServiceId}`)
  }
}
