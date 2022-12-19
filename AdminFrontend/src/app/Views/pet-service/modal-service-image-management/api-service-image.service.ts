import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { pagination } from 'src/app/Class/pagination';
import { petService } from 'src/app/Class/pet-service';
@Injectable({
  providedIn: 'root'
})
export class ApiServiceImageService {
  constructor(public http: HttpClient) { }
  getAll(serviceId: string) {
    const formData: FormData = new FormData();
    formData.append('serviceId', serviceId);
    return this.http.post<any>(environment.apiUrl+"PetServiceImage/getall",formData);
  }
  delete(id: string) {
    const formData: FormData = new FormData();
    formData.append('id', id);
    return this.http.post<any>(environment.apiUrl+"PetServiceImage/delete",formData);
  }
  addNew(files:File[], serviceId:string){
    const formData: FormData = new FormData();
    files.forEach((file)=>formData.append('files', file));
    formData.append('serviceId', serviceId);
    return this.http.post<any>(environment.apiUrl+"PetServiceImage/add",formData)
  }
}
