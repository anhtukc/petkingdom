import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ApiHeaderService {

  constructor(private http:HttpClient) { }
  public getPetService(){
      return this.http.get<any>(environment.apiUrl +"PetService"+ "/getAll");
  }
}
