import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { localStorageJwtHelper } from 'src/app/auth/local-storage-helper';
import { Schedule } from 'src/app/Class/Schedule';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiScheduleService {
  private controllerName: string = "Schedules/";
  constructor(public http: HttpClient, private localJwtHelper: localStorageJwtHelper) { }
  addNew(schedules: Schedule[]) {
    const httpOptions = this.localJwtHelper.getHttpOptions("frombody");
    return this.http.post<any>(environment.apiUrl + this.controllerName + "add", schedules, httpOptions)
  }
}
