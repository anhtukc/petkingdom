import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { pagination } from 'src/app/Class/pagination';
import { petService } from 'src/app/Class/pet-service';
import { basedSearchObject } from 'src/app/Class/SearchObjects';
import { localStorageJwtHelper } from 'src/app/Helper/local-storage-helper';
@Injectable({
  providedIn: 'root'
})
export class ApiPetService {
  private controllerName:string = "PetService/";
  constructor(public http: HttpClient, private localJwtHelper:localStorageJwtHelper) { }
  getAll() {
    return this.http.get<any>(environment.apiUrl +this.controllerName+ "getAll");
  }
  
  getPage(page: pagination) {
    const httpOptions = this.localJwtHelper.getHttpOptions("FromBody");
    return this.http.post<any>(environment.apiUrl +this.controllerName+ "getPage", page,httpOptions);
  }


  searchPage(page: pagination, searchObj:basedSearchObject) {
    const httpOptions = this.localJwtHelper.getHttpOptions("FromBody");
    const pObject ={page:page,searchObj:searchObj};
    return this.http.post<any>(environment.apiUrl +this.controllerName+ "search", JSON.stringify(pObject),httpOptions);
  }


  delete(id:string) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");

    const formData: FormData = new FormData();
    formData.set('id', id);
    return this.http.post<any>(environment.apiUrl +this.controllerName+ "delete", formData,httpOptions);
  }


  addNew(service: petService) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");

    const formData: FormData = new FormData();
    for (var key in service) {
      formData.append(key, service[key]);
    }
    formData.set('iconFile', service.iconFile, service.iconFile.name);

    return this.http.post<any>(environment.apiUrl +this.controllerName+"add", formData,httpOptions)
  }


  update(service: petService) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");

    const formData: FormData = new FormData();
    for (var key in service) {
      formData.append(key, service[key]);
    }
    if(service.iconFile !== null){
      formData.set('iconFile', service.iconFile, service.iconFile.name);
    }
    
    return this.http.post<any>(environment.apiUrl +this.controllerName+"update", formData,httpOptions)
  }

  getById(id:string){
    const httpOptions = this.localJwtHelper.getHttpOptions("default");

    return this.http.get<any>(environment.apiUrl +this.controllerName+`getById?id=${id}`,httpOptions)
  }
}
