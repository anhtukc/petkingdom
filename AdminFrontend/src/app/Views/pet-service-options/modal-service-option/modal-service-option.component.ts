import { Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { petService } from 'src/app/Class/pet-service';
import { ServiceOption } from 'src/app/Class/PetServiceOptions';
import { ApiPetService } from '../../pet-service/pet-service-api.service';
import { ApiServiceOptions } from '../api-service-options.service';
import { validateHelper } from 'src/app/Helper/validate-helper';
@Component({
  selector: 'app-modal-service-option',
  templateUrl: './modal-service-option.component.html',
  styleUrls: ['./modal-service-option.component.css']
})
export class ModalServiceOptionComponent implements OnInit {
  constructor(private modalService: NgbModal,
    private api: ApiServiceOptions,
    private PetServiceApi: ApiPetService,
    private validate:validateHelper) { }

  ngOnInit(): void {
    this.PetServiceApi.getAll().subscribe(result => {
      this.petService = result.list;
    });
  }

  public Editor = ClassicEditor;
  @Input() statusMeaning;
  @Output() updateEvent = new EventEmitter();
  @Output() createEvent = new EventEmitter();

  @ViewChild('modalPetServiceOption') optionModal: TemplateRef<any>;
  petService: petService[] = [];
  public option: ServiceOption = {
    id: '',
    petServiceId: '',
    name: '',
    description: '',
    estimatedCompletionTime: 0,
    status: 0,
    createdDate: null,
    updateDate: null,
    petServiceName: '',
  };
  public typeModal = '';
  public formGroup = new FormGroup({
    name: new FormControl(this.option.name, [
      Validators.required,
      Validators.minLength(4)
    ]),
    description: new FormControl(this.option.description, [
      Validators.required,
      Validators.minLength(10)
    ]),
    estimatedCompletionTime: new FormControl(this.option.estimatedCompletionTime, [
      Validators.required,
      Validators.pattern("^[0-9]*$"),
      Validators.min(5)
    ]),
    petServiceId: new FormControl(this.option.petServiceId,[
      Validators.required,
    
    ])
  });


  public name = this.formGroup.get('name');
  public description = this.formGroup.get('description');
  public estimatedCompletionTime = this.formGroup.get('estimatedCompletionTime');
  public petServiceId = this.formGroup.get('estimatedCompletionTime');

  getPetServiceName(){
    for(let i = 0;i <this.petService.length;i++){
      if(this.petService[i].id == this.option.petServiceId){
        this.option.petServiceName = this.petService[i].name;
      }
    }
  }

  public createNew() {
    this.option = {
      id: '',
      petServiceId: '',
      name: '',
      description: '',
      estimatedCompletionTime: 0,
      status: 0,
      createdDate: null,
      updateDate: null,
      petServiceName: '',
    };
  }

  public getById(id: string) {
    this.api.getById(id).subscribe(
      result => {
        this.option = result.obj;
      }
    )
  }

  public async create() {
    this.getPetServiceName();
    if (this.formGroup.valid) {
      this.api.addNew(this.option).subscribe(result => {
        if (result.status == 0) {
          alert("Thêm mới thất bại")
        }
        if (result.status == 1) {
          alert("Thêm mới thành công")
          this.option = result.obj;
          this.createEvent.emit(this.option);
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

  public async update(){
    this.getPetServiceName();
    if (this.formGroup.valid) {
      this.api.update(this.option).subscribe(result => {
        if (result.status == 0) {
          alert("Sửa thất bại")
        }
        if (result.status == 1) {
          alert("Sửa thành công")
          this.option = result.obj;
          this.updateEvent.emit(this.option);
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

  public openModal(typeModal: string, id?: string) {
    this.typeModal = typeModal;
    this.modalService.open(this.optionModal);
    if (typeModal == 'create') {
      this.createNew();
    }
    if (typeModal == 'edit') {
      this.getById(id);
    }
  }
}
