import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pet-service-options',
  templateUrl: './pet-service-options.component.html',
  styleUrls: ['./pet-service-options.component.css']
})
export class PetServiceOptionsComponent implements OnInit {
  // statusMeaning:Array<string>=["Không hoạt động", "Đang hoạt động"];
  // public page:pagination ={
  //   currentPage: 1,
  //   pageSize:20,
  //   sortColumn: "name",
  //   sortOrder: "ASC"
  // };
  constructor() { }

  ngOnInit(): void {
  }

}
