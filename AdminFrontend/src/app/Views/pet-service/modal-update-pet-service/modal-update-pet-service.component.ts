import { Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { petService } from 'src/app/Class/pet-service';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular';
import { ApiPetService } from '../pet-service-api.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-modal-update-pet-service',
  templateUrl: './modal-update-pet-service.component.html',
  styleUrls: ['./modal-update-pet-service.component.css']
})
export class ModalUpdatePetServiceComponent implements OnInit {
  @Input() statusMeaning;
  public Editor = ClassicEditor;
  @ViewChild('modalUpdatePetService') optionModal: TemplateRef<any>;
  @Output() updateEvent = new EventEmitter();
  previewImg = '';
  public petService: petService = {
    id: '',
    name: '',
    fullDescription: '',
    briefDescription: '',
    icon: '',
    iconFile: null,
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
      this.previewImg = URL.createObjectURL(files[0]);
    }
  }
  constructor(private modalService: NgbModal, private api: ApiPetService) { }
  ngOnInit(): void {

  }

  onChange({ editor }: ChangeEvent) {
    const data = editor.getData();
    this.petService.briefDescription = data;
  }
  public formGroup = new FormGroup({
    name: new FormControl(this.petService.name, [
      Validators.required,
      Validators.minLength(4)
    ]),
    fullDescription: new FormControl(this.petService.fullDescription, [
      Validators.required,
      Validators.minLength(10)
    ]),
    briefDescription: new FormControl(this.petService.briefDescription, [
      Validators.required,
      Validators.minLength(10)
    ])
   
  });
  
  public name = this.formGroup.get('name');
  public briefDescription = this.formGroup.get('briefDescription');
  public fullDescription = this.formGroup.get('fullDescription');

  public async save() {
    this.api.update(this.petService).subscribe(result => {
      if (result.status == 0) {
        alert("Lưu thất bại")
      }
      if (result.status == 1) {
        alert("Lưu thành công")
        this.petService = result.obj;
        this.updateEvent.emit(this.petService);
        this.modalSetUp();
      }
    })
  }

  public async openModal(id: string) {

    this.modalService.open(this.optionModal);
    this.api.getById(id).subscribe(
      result => {
        this.petService = result.obj;
      }
    )
  }

  modalSetUp() {
    this.previewImg = '';
  }

}
