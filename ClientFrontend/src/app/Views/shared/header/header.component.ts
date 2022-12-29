import { Component, OnInit } from '@angular/core';
import { petService } from 'src/app/Class/pet-service';
import { ApiHeaderService } from './api-header.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private apiheader:ApiHeaderService) { }
  petServiceHeader:petService[] = []
  ngOnInit(): void {
    this.apiheader.getPetService().subscribe(result=>{
      this.petServiceHeader =result.list;
    })
  }

}
