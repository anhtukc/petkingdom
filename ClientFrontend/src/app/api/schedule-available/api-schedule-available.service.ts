import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiScheduleAvailableService {
  private controllerName = "ScheduleAvailables/";
  constructor(private http:HttpClient) { }

  public getScheduleByOptionId(id:string, startedDateFormat:string){
    return this.http.get<any>(environment.apiUrl + this.controllerName + `getByOptionId?id=${id}&&startedDateFormat=${startedDateFormat}`);
  }
}
