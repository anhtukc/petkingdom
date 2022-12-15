import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { pagination } from 'src/app/Class/pagination';
@Injectable({
  providedIn: 'root'
})
export class PetServiceApiService {
  constructor(public http: HttpClient) { }
  GetPage(page: pagination) {
    return this.http.post<any>(environment.apiUrl+"PetService/GetPage",page);
  }
  getsanphammoi():Observable<any>{
    return this.http.get<any>(environment.apiUrl+"sanphams/topsanphammoi")
  }
  
}
