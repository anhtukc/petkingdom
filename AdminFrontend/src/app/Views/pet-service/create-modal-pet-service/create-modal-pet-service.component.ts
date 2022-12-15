import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Guid } from 'guid-typescript';
import { petService } from 'src/app/Class/pet-service';

@Component({
  selector: 'app-create-modal-pet-service',
  templateUrl: './create-modal-pet-service.component.html',
  styleUrls: ['./create-modal-pet-service.component.css']
})
export class CreateModalPetServiceComponent implements OnInit {
  @ViewChild('createModalPetService') optionModal : TemplateRef<any>; 
  public petService:petService = {
    id:Guid.create(),
    name:'',
    fullDesciption:'',
    briefDescription:'',
    thumbnail:'',
    icon:'',
    status: 0
  };
  public heroForm = new FormGroup({
    name: new FormControl(this.petService.name, [
      Validators.required,
      Validators.minLength(4),
    ]),
    
  });
  constructor(private modalService: NgbModal) { }

  public openModal(){
    this.modalService.open(this.optionModal);
  }
  ngOnInit(): void {
  }

}
