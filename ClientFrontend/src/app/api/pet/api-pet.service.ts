import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { localStorageJwtHelper } from 'src/app/auth/local-storage-helper';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiPet {
  private controllerName: string = "Pets/";
  constructor(public http: HttpClient, private localJwtHelper: localStorageJwtHelper) { }



  delete(id: string) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");
    const formData: FormData = new FormData();
    formData.set('id', id);
    return this.http.post<any>(environment.apiUrl + this.controllerName + "delete", formData, httpOptions);
  }

  addNew(pet: any) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");
    const formData: FormData = new FormData();
    for (var key in pet) {
      formData.append(key, pet[key]);
    }
    return this.http.post<any>(environment.apiUrl + this.controllerName + "add", formData, httpOptions)
  }


  update(pet: any) {
    const HttpOptions = this.localJwtHelper.getHttpOptions("default");

    const formData: FormData = new FormData();
    for (var key in pet) {
      formData.append(key, pet[key]);
    }
    return this.http.post<any>(environment.apiUrl + this.controllerName + "update", formData, HttpOptions)
  }

  
  getById(id: string) {
    const HttpOptions = this.localJwtHelper.getHttpOptions("default");
    return this.http.get<any>(environment.apiUrl + this.controllerName + `getById?id=${id}`, HttpOptions)
  }
  getByCustomerId(customerId:string){
    const HttpOptions = this.localJwtHelper.getHttpOptions("default");
    return this.http.get<any>(environment.apiUrl + this.controllerName + `getByCustomerId?id=${customerId}`, HttpOptions)
  }
}
