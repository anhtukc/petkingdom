import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { pagination } from 'src/app/Class/pagination';
import { basedSearchObject } from 'src/app/Class/SearchObjects';
import { localStorageJwtHelper } from 'src/app/Helper/local-storage-helper';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiBlogService {

  private controllerName: string = "Blogs/";
  constructor(public http: HttpClient, private localJwtHelper: localStorageJwtHelper) { }

  getPage(page: pagination) {
    const httpOptions = this.localJwtHelper.getHttpOptions("frombody");
    return this.http.post<any>(environment.apiUrl + this.controllerName + "getPage", page, httpOptions);
  }

  searchPage(page: pagination, searchObj: basedSearchObject) {
    const httpOptions = this.localJwtHelper.getHttpOptions("frombody");
    const pObject = { page: page, searchObj: searchObj};
    return this.http.post<any>(environment.apiUrl + this.controllerName + "search", pObject, httpOptions);
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
    if(sa.file !== null){
      formData.set('file', sa.file, sa.file.name);
    }
    return this.http.post<any>(environment.apiUrl + this.controllerName + "add", formData, httpOptions)
  }


  update(sa: any) {
    const HttpOptions = this.localJwtHelper.getHttpOptions("default");

    const formData: FormData = new FormData();
    for (var key in sa) {
      formData.append(key, sa[key]);
    }
    if(sa.file !== null){
      formData.set('file', sa.file, sa.file.name);
    }
    return this.http.post<any>(environment.apiUrl + this.controllerName + "update", formData, HttpOptions)
  }

  
  getById(id: string) {
    const HttpOptions = this.localJwtHelper.getHttpOptions("default");

    return this.http.get<any>(environment.apiUrl + this.controllerName + `getById?id=${id}`, HttpOptions)
  }
}
