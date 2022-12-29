import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { localStorageJwtHelper } from 'src/app/auth/local-storage-helper';
import { Account } from 'src/app/Class/Account';
import { Customer } from 'src/app/Class/Customer';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiCustomerService {

    private controllerName: string = "Customers/";
    constructor(public http: HttpClient, private localJwtHelper: localStorageJwtHelper) { }
  
  
    addNew(sa: Customer, acc:Account) {
      const httpOptions = this.localJwtHelper.getHttpOptions("frombody");
      sa.file = undefined;
  
      const ca = { cus: sa, acc: acc, file:sa.image};
      return this.http.post<any>(environment.apiUrl + this.controllerName + "add", ca, httpOptions)
    }
    
    getById(id: string) {
      const HttpOptions = this.localJwtHelper.getHttpOptions("default");
  
      return this.http.get<any>(environment.apiUrl + this.controllerName + `getById?id=${id}`, HttpOptions)
    }
  }
  

