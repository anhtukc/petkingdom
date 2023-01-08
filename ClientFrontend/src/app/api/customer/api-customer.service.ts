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
  
  
    addNew(sa: Customer) {
      const HttpOptions = this.localJwtHelper.getHttpOptions("default");
  
      const formData: FormData = new FormData();
      for (var key in sa) {
        formData.append(key, sa[key]);
      }
      if(sa.file !== null){
        formData.set('file', sa.file, sa.file.name);
      }
      return this.http.post<any>(environment.apiUrl + this.controllerName + "add", formData, HttpOptions)
    }
    
    getById(id: string) {
      const HttpOptions = this.localJwtHelper.getHttpOptions("default");
  
      return this.http.get<any>(environment.apiUrl + this.controllerName + `getById?id=${id}`, HttpOptions)
    }
  }
  

