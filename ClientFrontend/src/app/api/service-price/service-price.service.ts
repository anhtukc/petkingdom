import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiServicePrice {
  controllerName ="ServiceSellPrices/"
  constructor(private http:HttpClient) { }
  getSellPrice(optionId:string){
    return this.http.get<any>(environment.apiUrl +this.controllerName+ `getByOptionId?id=${optionId}`)
  }
  public getAllPrice(){
    return this.http.get<any>(environment.apiUrl +this.controllerName+ "getAll");
  }
}
