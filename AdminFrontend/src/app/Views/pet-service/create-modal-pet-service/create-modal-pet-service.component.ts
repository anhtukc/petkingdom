import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Guid } from 'guid-typescript';
import { CustomValidators } from 'ng2-validation';
import { petService } from 'src/app/Class/pet-service';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular';
@Component({
  selector: 'app-create-modal-pet-service',
  templateUrl: './create-modal-pet-service.component.html',
  styleUrls: ['./create-modal-pet-service.component.css']
})
export class CreateModalPetServiceComponent implements OnInit {
  public Editor = ClassicEditor;
  @ViewChild('createModalPetService') optionModal : TemplateRef<any>; 
  public petService:petService = {
    id:'',
    name:'',
    fullDesciption:'',
    briefDescription:'<p>test</p>',
    icon:'',
    status: 0
  };
  public heroForm = new FormGroup({
    name: new FormControl(this.petService.name, [
      Validators.required,
      Validators.minLength(4)
    ]),
    fullDesciption:new FormControl(this.petService.fullDesciption,[
      Validators.required,
      Validators.minLength(4)
    ]),
    briefDescription:new FormControl(this.petService.briefDescription,[
      Validators.required,
      Validators.minLength(4)
    ]),
    status:new FormControl(this.petService.status,[
      Validators.required,
      CustomValidators.rangeLength([1, 3])
    ])
  });
  constructor(private modalService: NgbModal) { }

  public openModal(){
    this.modalService.open(this.optionModal);
  }
   onChange( { editor }: ChangeEvent ) {
    const data = editor.getData();
    this.petService.briefDescription = data;
  }
  public check(){
    console.log(this.petService);
  }
  ngOnInit(): void {
    
  }

}
