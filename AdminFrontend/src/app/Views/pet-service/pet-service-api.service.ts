import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { pagination } from 'src/app/Class/pagination';
import { petService } from 'src/app/Class/pet-service';
@Injectable({
  providedIn: 'root'
})
export class PetServiceApiService {
  private controllerName:string = "PetService/";
  constructor(public http: HttpClient) { }
  getPage(page: pagination) {
    return this.http.post<any>(environment.apiUrl +this.controllerName+ "getPage", page);
  }
  delete(id:string) {
    const formData: FormData = new FormData();
    formData.set('id', id);
    return this.http.post<any>(environment.apiUrl +this.controllerName+ "delete", formData);
  }
  addNew(service: petService) {
    const formData: FormData = new FormData();
    for (var key in service) {
      formData.append(key, service[key]);
    }
    formData.set('iconFile', service.iconFile, service.iconFile.name);
    return this.http.post<any>(environment.apiUrl +this.controllerName+"add", formData)
  }
  update(service: petService) {
    const formData: FormData = new FormData();
    for (var key in service) {
      formData.append(key, service[key]);
    }
    if(service.iconFile !== null){
      formData.set('iconFile', service.iconFile, service.iconFile.name);
    }
    return this.http.post<any>(environment.apiUrl +this.controllerName+"update", formData)
  }

  getById(id:string){
    const formData: FormData = new FormData();
    formData.set('id', id);
    return this.http.get<any>(environment.apiUrl +this.controllerName+`getById?id=${id}`)
  }
}
