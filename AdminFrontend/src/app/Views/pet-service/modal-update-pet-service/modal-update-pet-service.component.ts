import { Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { petService } from 'src/app/Class/pet-service';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular';
import { PetServiceApiService } from '../pet-service-api.service';

@Component({
  selector: 'app-modal-update-pet-service',
  templateUrl: './modal-update-pet-service.component.html',
  styleUrls: ['./modal-update-pet-service.component.css']
})
export class ModalUpdatePetServiceComponent implements OnInit {
  @Input() statusMeaning;
  public Editor = ClassicEditor;
  @ViewChild('modalUpdatePetService') optionModal : TemplateRef<any>; 
  @Output() updateEvent = new EventEmitter();
  previewImg = '';
  public petService:petService = {
    id:'',
    name:'',
    fullDescription:'',
    briefDescription:'',
    icon:'',
    iconFile:null,
    status: 0
  };
  // public heroForm = new FormGroup({
  //   name: new FormControl(this.petService.name, [
  //     Validators.required,
  //     Validators.minLength(4)
  //   ]),
  //   fullDesciption:new FormControl(this.petService.fullDescription,[
  //     Validators.required,
  //     Validators.minLength(4)
  //   ]),
  //   briefDescription:new FormControl(this.petService.briefDescription,[
  //     Validators.required,
  //     Validators.minLength(4)
  //   ]),
  //   status:new FormControl(this.petService.status,[
  //     Validators.required,
  //     CustomValidators.rangeLength([0, 3])
  //   ])
  // });
  upload(event: Event) {
    const target = event.target as HTMLInputElement;
    const files = target.files as FileList;
    this.petService.iconFile = files[0];
    if (files.length > 0) {
      this.previewImg= URL.createObjectURL(files[0]);
    }
}
  constructor(private modalService: NgbModal, private api:PetServiceApiService) { }
  ngOnInit(): void {
    
  }

   onChange( { editor }: ChangeEvent ) {
    const data = editor.getData();
    this.petService.briefDescription = data;
  }
   public async save(){
    this.api.update(this.petService).subscribe(result=>{
      if(result.message == "fail"){
        alert("Lưu thất bại")
      }
      if(result.message=="success"){
        alert("Lưu thành công")
        this.petService = result.obj;
      this.updateEvent.emit(this.petService);
      this.modalSetUp();
      
      }
      
    })
   
  }
 
   public async openModal(id:string){
    this.modalService.open(this.optionModal);
    this.api.getById(id).subscribe(
      result=>{
        this.petService = result.obj;
      }
    )
  }

  modalSetUp(){
    this.previewImg ='';
  }

}
