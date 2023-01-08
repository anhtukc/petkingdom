import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { localStorageJwtHelper } from 'src/app/auth/local-storage-helper';
import { Account } from 'src/app/Class/Account';
import { SellBill } from 'src/app/Class/SellBill';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiSellbillService {
  private controllerName: string = "Sellbills/";
  constructor(public http: HttpClient, private localJwtHelper: localStorageJwtHelper) { }
  addNew(sb: any) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");
    let formData: FormData = new FormData();
    for (var key in sb) {
      formData.append(key, sb[key]);
    }
    return this.http.post<any>(environment.apiUrl + this.controllerName + "add", formData, httpOptions)
  }
  
}
