import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiServicePrice {

  constructor(private http:HttpClient) { }
  getSellPrice(optionId:string){
    return this.http.get<any>(environment.apiUrl +"ServiceSellPrices"+ `/getByOptionId?id=${optionId}`)
  }
}
