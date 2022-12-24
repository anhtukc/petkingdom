import { Component, EventEmitter, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { petService } from 'src/app/Class/pet-service';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular';
import { PetServiceApiService } from '../pet-service-api.service';
import { CustomValidators } from 'ng2-validation';

@Component({
  selector: 'app-create-modal-pet-service',
  templateUrl: './create-modal-pet-service.component.html',
  styleUrls: ['./create-modal-pet-service.component.css']
})
export class CreateModalPetServiceComponent implements OnInit {
  public Editor = ClassicEditor;
  @ViewChild('createModalPetService') optionModal: TemplateRef<any>;
  @Output() addEvent = new EventEmitter();
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
    ]),
    fileControl: new FormControl(this.petService.iconFile, [
      Validators.required,
      Validators.pattern(/\.webp$|\.img$|\.png$/i)
    ])
  });
  
  public name = this.formGroup.get('name');
  public briefDescription = this.formGroup.get('briefDescription');
  public fullDescription = this.formGroup.get('fullDescription');
  public fileControl = this.formGroup.get('fileControl');


  upload(event: Event) {
    const target = event.target as HTMLInputElement;
    const files = target.files as FileList;
    this.petService.iconFile = files[0];
    if (files.length > 0) {
      this.previewImg = URL.createObjectURL(files[0]);
    }
  }
  constructor(private modalService: NgbModal, private api: PetServiceApiService) { }
  ngOnInit(): void {

  }

  onChange({ editor }: ChangeEvent) {
    const data = editor.getData();
    this.petService.briefDescription = data;
  }

  public async save() {
    if (this.formGroup.valid) {
      this.api.addNew(this.petService).subscribe(result => {
        if (result.status == 0) {
          alert("Lưu thất bại")
        }
        if (result.status == 1) {
          alert("Lưu thành công")
          this.petService = result.obj;
          this.addEvent.emit(this.petService);
        }
      })
    }
    else {
      Object.keys(this.formGroup.controls).forEach(key => {
        this.formGroup.controls[key].markAsTouched();
        this.formGroup.controls[key].markAsDirty();
      });
    }
  }


  public openModal() {
    this.modalService.open(this.optionModal);
  }
}
