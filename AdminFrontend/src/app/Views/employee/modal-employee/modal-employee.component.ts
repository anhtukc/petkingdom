import { Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ApiEmployee } from '../api-employee.service';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { Employee } from 'src/app/Class/Employee';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-modal-employee',
  templateUrl: './modal-employee.component.html',
  styleUrls: ['./modal-employee.component.css']
})
export class ModalEmployeeComponent implements OnInit {
  constructor(private modalService: NgbModal,
    private api: ApiEmployee,private datePipe:DatePipe) { }

  ngOnInit(): void {

  }
public sex=['nam', 'nữ'];
  public Editor = ClassicEditor;
  @Input() statusMeaning;
  @Output() updateEvent = new EventEmitter();
  @Output() createEvent = new EventEmitter();

  @ViewChild('modalEmployee') optionModal: TemplateRef<any>;
  public emp: Employee = {
    id: '',
    firstName: '',
    lastName: '',
    phonenumber: '',
    address: '',
    email: '',
    birthday: null,
    identityCardNumber: '',
    sex: '',
    status: 2,
    birthdayFormat:'',
    accountId:'1'
  };
  public typeModal = '';
   
  public formGroup = new FormGroup({
    firstName: new FormControl(this.emp.firstName, [
      Validators.required
    ]),
    lastName: new FormControl(this.emp.lastName, [
      Validators.required
    ]),
    phonenumber: new FormControl(this.emp.phonenumber, [
      Validators.required,
      Validators.pattern('^(84|0[3|5|7|8|9])+([0-9]{8})$')
    ]),
    address: new FormControl(this.emp.address, [
      Validators.required,
      Validators.minLength(10)
    ]),
    email: new FormControl(this.emp.email,[
      Validators.email,
      Validators.required
    ]),
    birthdayFormat: new FormControl(this.emp.birthdayFormat,[
      Validators.required
    ]),
    identityCardNumber: new FormControl(this.emp.identityCardNumber,[
      Validators.required
    ])
  });


  public firstName = this.formGroup.get('firstName');
  public lastName = this.formGroup.get('lastName');
  public phonenumber = this.formGroup.get('phonenumber');
  public address = this.formGroup.get('address');
  public email = this.formGroup.get('email');
  public birthdayFormat = this.formGroup.get('birthdayFormat');
  public identityCardNumber = this.formGroup.get('identityCardNumber');



  public createNew() {
    this.emp = {
      id: '',
      firstName: '',
      lastName: '',
      phonenumber: '',
      address: '',
      email: '',
      birthday: null,
      identityCardNumber: '',
      sex: '',
      status: 2,
    birthdayFormat:'',
    accountId:'1'
    };
  }

  public getById(id: string) {
    this.api.getById(id).subscribe(
      result => {
        this.emp = result.obj;
        this.emp.birthdayFormat = this.datePipe.transform(this.emp.birthday, 'yyyy-MM-dd');
        console.log(this.emp.birthdayFormat);
      }
    )
   
  }

  public async create() {

    if (this.formGroup.valid) {
      this.api.addNew(this.emp).subscribe(result => {
        if (result.status == 0) {
          alert("Thêm mới thất bại")
        }
        if (result.status == 1) {
          alert("Thêm mới thành công")
          this.emp = result.obj;
          this.createEvent.emit(this.emp);
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

  alert(){
    console.log(this.emp.sex)
  }
  public async update() {
    if (this.formGroup.valid) {
      this.api.update(this.emp).subscribe(result => {
        if (result.status == 0) {
          alert("Sửa thất bại")
        }
        if (result.status == 1) {
          alert("Sửa thành công")
          this.emp = result.obj;
          this.updateEvent.emit(this.emp);
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
