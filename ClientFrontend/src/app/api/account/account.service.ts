import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { localStorageJwtHelper } from 'src/app/auth/local-storage-helper';
import { login } from 'src/app/Class/login';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiAccountService {
  private controllerName: string = "Accounts/";
  constructor(private http:HttpClient, private localJwtHelper: localStorageJwtHelper) { }
  checkCustomerAccount(username:string, email:string, phonenumber:string) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");
    return this.http.get<any>(environment.apiUrl + this.controllerName + `checkCustomerAccount?username=${username}&&email=${email}&&phonenumber=${phonenumber}`, httpOptions);
  }
  login(acc:login) {
    return this.http.post<any>(environment.apiUrl+"Authencation/Authenticate",acc);
  }
  addNew(sa: any) {
    const httpOptions = this.localJwtHelper.getHttpOptions("default");

    const formData: FormData = new FormData();
    for (var key in sa) {
      formData.append(key, sa[key]);
    }
    return this.http.post<any>(environment.apiUrl + this.controllerName + "add", formData, httpOptions)
  }
}
