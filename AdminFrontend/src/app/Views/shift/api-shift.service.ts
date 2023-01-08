import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { pagination } from 'src/app/Class/pagination';
import { basedSearchObject } from 'src/app/Class/SearchObjects';
import { localStorageJwtHelper } from 'src/app/Helper/local-storage-helper';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiShiftService {
  private controllerName: string = "Shifts/";
  constructor(public http: HttpClient, private localJwtHelper: localStorageJwtHelper) { }

  getPageByUserId(page: pagination, searchObj: basedSearchObject, accountId:string) {
    const httpOptions = this.localJwtHelper.getHttpOptions("frombody");
    const pObject = { page: page, searchObj: searchObj, Id:accountId };
    return this.http.post<any>(environment.apiUrl + this.controllerName + "getPageByUserId", pObject, httpOptions);
  }
  getPage(page: pagination) {
    const httpOptions = this.localJwtHelper.getHttpOptions("frombody");
    return this.http.post<any>(environment.apiUrl + this.controllerName + "getPage", page, httpOptions);
  }

  searchPage(page: pagination, searchObj: basedSearchObject) {
    const httpOptions = this.localJwtHelper.getHttpOptions("frombody");
    const pObject = { page: page, searchObj: searchObj };
    return this.http.post<any>(environment.apiUrl + this.controllerName + "search", JSON.stringify(pObject), httpOptions);
  }
  update(sa: any) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");

    const formData: FormData = new FormData();
    for (var key in sa) {
      formData.append(key, sa[key]);
    }
    return this.http.post<any>(environment.apiUrl + this.controllerName + "update", formData, httpOptions)
  }
  delete(id: string) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");

    const formData: FormData = new FormData();
    formData.set('id', id);
    return this.http.post<any>(environment.apiUrl + this.controllerName + "delete", formData, httpOptions);
  }
  addNew(sa: any) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");

    const formData: FormData = new FormData();
    for (var key in sa) {
      formData.append(key, sa[key]);
    }
    return this.http.post<any>(environment.apiUrl + this.controllerName + "add", formData, httpOptions)
  }


  
  
}
