import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { login } from 'src/app/Class/login';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiAccountService {

  constructor(private http:HttpClient) { }
  login(acc:login) {
    return this.http.post<any>(environment.apiUrl+"Authencation/Authenticate",acc);
  }
}
