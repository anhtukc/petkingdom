import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Customer } from 'src/app/Class/Customer';
import { ApiCustomerService } from '../api-customer.service';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { DomSanitizer } from '@angular/platform-browser';
import { Account } from 'src/app/Class/Account';
import { ApiAccountService } from 'src/app/api/account/api-account.service';
@Component({
  selector: 'app-modal-customer',
  templateUrl: './modal-customer.component.html',
  styleUrls: ['./modal-customer.component.css']
})
export class ModalCustomerComponent implements OnInit {

  constructor(private modalService: NgbModal,
    private api: ApiCustomerService,private apiAccount:ApiAccountService) { }

  ngOnInit(): void {

  }

  alert(){
    console.log(this.customer.sex)
  }
  public sex:string[]=['nam', 'nữ'];
  public Editor = ClassicEditor;
  @Input() statusMeaning;
  @Output() updateEvent = new EventEmitter();
  @Output() createEvent = new EventEmitter();

  @ViewChild('modalCustomer') optionModal: TemplateRef<any>;
  public customer: Customer = {
    id: '',
    firstName: '',
    lastName: '',
    phonenumber: '',
    address: '',
    email: '',
    image: '',
    file: null,
    sex: 'nam',
    status: 0,
    accountId:'1',
    
  };
  account:Account ={
    id:'',
    username:'demo0101',
    password:'demo0101',
    permission:'customer',
    phoneConfirmed:true
  }
  public typeModal = '';
  upload(event: Event) {
    const target = event.target as HTMLInputElement;
    const files = target.files as FileList;
    this.customer.file = files[0];
    const reader = new FileReader();
    reader.readAsDataURL(this.customer.file);
    reader.onload = () => {
      this.customer.image = reader.result as string;
    };
    
  }
  public formGroup = new FormGroup({
    firstName: new FormControl(this.customer.firstName, [
      Validators.required
    ]),
    lastName: new FormControl(this.customer.lastName, [
      Validators.required
    ]),
    phonenumber: new FormControl(this.customer.phonenumber, [
      Validators.required,
      Validators.pattern('^(84|0[3|5|7|8|9])+([0-9]{8})$')
    ]),
    address: new FormControl(this.customer.address, [
      Validators.required,
      Validators.minLength(10)
    ]),
    email: new FormControl(this.customer.email,[
      Validators.email,
      Validators.required
    ]),
    username: new FormControl(this.account.username,[
      Validators.required,
      Validators.minLength(8)
    ]),
    password: new FormControl(this.account.password,[
      Validators.required,
      Validators.minLength(8)
    ])
  });


  public firstName = this.formGroup.get('firstName');
  public lastName = this.formGroup.get('lastName');
  public phonenumber = this.formGroup.get('phonenumber');
  public address = this.formGroup.get('address');
  public email = this.formGroup.get('email');
  public username = this.formGroup.get('username');
  public password = this.formGroup.get('password');




  public createNew() {
    this.customer = {
      id: '',
      firstName: '',
      lastName: '',
      phonenumber: '',
      address: '',
      email: '',
      image: '',
      file: null,
      sex: '',
      status: 0,
      accountId:'1'
      
    };
    this.account ={
      id:'',
      username:'demo0101',
      password:'demo0101',
      permission:'customer',
      phoneConfirmed:true
    }
  }

  public getById(id: string) {
    this.api.getById(id).subscribe(
      result => {
        this.customer = result.obj;      
      }
    )
   
  }

  public async create() {

    if (this.formGroup.valid ) {
      this.apiAccount.checkCustomerAccount(this.account.username, this.customer.email, this.customer.phonenumber).subscribe(result=>{
        if(result.status == 1){
          this.apiAccount.addNew(this.account).subscribe(resultacc=>{
            this.account = resultacc.obj;
          })
          this.customer.accountId = this.account.id;
          this.api.addNew(this.customer).subscribe(result => {
            if (result.status == 0) {
              alert("Thêm mới thất bại")
            }
            if (result.status == 1) {
              alert("Thêm mới thành công")
              this.customer = result.obj;
              this.createEvent.emit(this.customer);
            }
          })
        }
        else{
          alert(result.message);
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


  public async update() {
    this.account.username = '132323233';
    this.account.password = '122222222';
    if (this.formGroup.valid ) {
      this.api.update(this.customer).subscribe(result => {
        if (result.status == 0) {
          alert("Sửa thất bại")
        }
        if (result.status == 1) {
          alert("Sửa thành công")
          this.customer = result.obj;
          this.updateEvent.emit(this.customer);
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
