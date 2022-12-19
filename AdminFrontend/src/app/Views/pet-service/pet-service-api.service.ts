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
  constructor(public http: HttpClient) { }
  getPage(page: pagination) {
    return this.http.post<any>(environment.apiUrl+"PetService/getPage",page);
  }
  addNew(service:petService){
    const formData: FormData = new FormData();
    for ( var key in service ) {
      formData.append(key, service[key]);
  }
    formData.set('iconFile', service.iconFile, service.iconFile.name);
    return this.http.post<any>(environment.apiUrl+"PetService/add",formData)
  }
}
