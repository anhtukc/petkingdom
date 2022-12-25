import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { pagination } from 'src/app/Class/pagination';
import { petService } from 'src/app/Class/pet-service';
import { basedSearchObject } from 'src/app/Class/SearchObjects';
import { environment } from 'src/environments/environment';
import { localStorageJwtHelper } from 'src/app/Helper/local-storage-helper';
import { ServiceOption } from 'src/app/Class/PetServiceOptions';
@Injectable({
  providedIn: 'root'
})
export class ApiServiceOptions {
  private controllerName: string = "PetServiceOption/";
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

  addNew(service: ServiceOption) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");

    const formData: FormData = new FormData();
    for (var key in service) {
      formData.append(key, service[key]);
    }
    return this.http.post<any>(environment.apiUrl + this.controllerName + "add", formData, httpOptions)
  }


  update(service: ServiceOption) {
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
