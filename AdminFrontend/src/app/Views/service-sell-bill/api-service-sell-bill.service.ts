import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, timestamp } from 'rxjs';
import { Socket } from 'ngx-socket-io';
import { CountStatus } from 'src/app/Class/CountStatus';
import { pagination } from 'src/app/Class/pagination';
import { basedSearchObject } from 'src/app/Class/SearchObjects';
import { SellBill } from 'src/app/Class/SellBill';
import { localStorageJwtHelper } from 'src/app/Helper/local-storage-helper';
import { environment } from 'src/environments/environment';
import * as signalR from "@microsoft/signalr";

@Injectable({
  providedIn: 'root'
})
export class ApiServiceSellBillService {

  private controllerName: string = "SellBills/";

  constructor(public http: HttpClient, private localJwtHelper: localStorageJwtHelper) {
 
  }
  getPage(page: pagination, searchObject: basedSearchObject) {
    const httpOptions = this.localJwtHelper.getHttpOptions("FromBody");
    let pObject = { page: page, searchObj: searchObject };
    return this.http.post(environment.apiUrl + this.controllerName + 'getPage', pObject, httpOptions);
  }

  GetAllStatus() {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");
    return this.http.get(environment.apiUrl + this.controllerName + 'getStatus', httpOptions);

  }
  
  delete(id: string) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");
    const formData: FormData = new FormData();
    formData.set('id', id);
    return this.http.post<any>(environment.apiUrl + this.controllerName + "delete", formData, httpOptions);
  }


  addNew(bill: any) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");

    const formData: FormData = new FormData();
    for (var key in bill) {
      formData.append(key, bill[key]);
    }

    return this.http.post<any>(environment.apiUrl + this.controllerName + "add", formData, httpOptions)
  }


  update(bill: any) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");
    const formData: FormData = new FormData();
    for (var key in bill) {
      formData.append(key, bill[key]);
    }

    return this.http.post<any>(environment.apiUrl + this.controllerName + "update", formData, httpOptions)
  }

  getById(id: string) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");

    return this.http.get<any>(environment.apiUrl + this.controllerName + `getById?id=${id}`, httpOptions)
  }
  destroy(){
  }
}
