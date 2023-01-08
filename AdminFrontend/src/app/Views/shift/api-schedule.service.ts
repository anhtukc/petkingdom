import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { pagination } from 'src/app/Class/pagination';
import { Schedule } from 'src/app/Class/Schedule';
import { basedSearchObject } from 'src/app/Class/SearchObjects';
import { localStorageJwtHelper } from 'src/app/Helper/local-storage-helper';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiScheduleService {
  private controllerName: string = "Schedules/";
  constructor(public http: HttpClient, private localJwtHelper: localStorageJwtHelper) { }

  getPage(page: pagination) {
    const httpOptions = this.localJwtHelper.getHttpOptions("frombody");
    return this.http.post<any>(environment.apiUrl + this.controllerName + "getPage", page, httpOptions);
  }

  searchPage(page: pagination, searchObj: basedSearchObject) {
    const httpOptions = this.localJwtHelper.getHttpOptions("frombody");
    const pObject = { page: page, searchObj: searchObj };
    return this.http.post<any>(environment.apiUrl + this.controllerName + "search", JSON.stringify(pObject), httpOptions);
  }

  delete(id: string) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");

    const formData: FormData = new FormData();
    formData.set('id', id);
    return this.http.post<any>(environment.apiUrl + this.controllerName + "delete", formData, httpOptions);
  }
  addNew(schedules: Schedule[]) {
    const httpOptions = this.localJwtHelper.getHttpOptions("frombody");
    return this.http.post<any>(environment.apiUrl + this.controllerName + "add", schedules, httpOptions)
  }

  update(service: any) {
    const HttpOptions = this.localJwtHelper.getHttpOptions("default");

    const formData: FormData = new FormData();
    for (var key in service) {
      formData.append(key, service[key]);
    }
    return this.http.post<any>(environment.apiUrl + this.controllerName + "update", formData, HttpOptions)
  }

  
  getById(id: string) {
    const HttpOptions = this.localJwtHelper.getHttpOptions("default");

    return this.http.get<any>(environment.apiUrl + this.controllerName + `getById?id=${id}`, HttpOptions)
  }
}
