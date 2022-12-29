import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { pagination } from 'src/app/Class/pagination';
import { basedSearchObject, postingObjectWithId } from 'src/app/Class/SearchObjects';
import { ServiceSellPrice } from 'src/app/Class/ServiceSellPrice';
import { localStorageJwtHelper } from 'src/app/Helper/local-storage-helper';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiServiceOptionPrice {
  private controllerName: string = "ServiceSellPrices/";
  constructor(public http: HttpClient, private localJwtHelper: localStorageJwtHelper) { }

  getPage(page: pagination, optionId:string) {
    const httpOptions = this.localJwtHelper.getHttpOptions("frombody");
    const pObject:postingObjectWithId = { page: page, searchObj: null, id:optionId};
    return this.http.post<any>(environment.apiUrl + this.controllerName + "getPage", pObject, httpOptions);
  }

  searchPage(page: pagination, searchObj: basedSearchObject, optionId:string) {
    const httpOptions = this.localJwtHelper.getHttpOptions("frombody");
    const pObject:postingObjectWithId = { page: page, searchObj: searchObj, id:optionId};
    return this.http.post<any>(environment.apiUrl + this.controllerName + "search", pObject, httpOptions);
  }

  delete(id: string) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");

    const formData: FormData = new FormData();
    formData.set('id', id);
    return this.http.post<any>(environment.apiUrl + this.controllerName + "delete", formData, httpOptions);
  }

  addNew(sa: ServiceSellPrice) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");

    const formData: FormData = new FormData();
    for (var key in sa) {
      formData.append(key, sa[key]);
    }
    return this.http.post<any>(environment.apiUrl + this.controllerName + "add", formData, httpOptions)
  }


  update(sa: ServiceSellPrice) {
    const HttpOptions = this.localJwtHelper.getHttpOptions("default");

    const formData: FormData = new FormData();
    for (var key in sa) {
      formData.append(key, sa[key]);
    }
    return this.http.post<any>(environment.apiUrl + this.controllerName + "update", formData, HttpOptions)
  }

  
  getById(id: string) {
    const HttpOptions = this.localJwtHelper.getHttpOptions("default");

    return this.http.get<any>(environment.apiUrl + this.controllerName + `getById?id=${id}`, HttpOptions)
  }
}
