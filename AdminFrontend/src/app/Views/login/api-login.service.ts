import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { pagination } from 'src/app/Class/pagination';
import { login } from 'src/app/Class/login';
@Injectable({
  providedIn: 'root'
})
export class LoginApiService {
  constructor(public http: HttpClient) { }
  login(acc:login) {
    return this.http.post<any>(environment.apiUrl+"Authencation/Authenticate",acc);
  }

  
}
