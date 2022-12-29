
import { animate, style, transition, trigger } from '@angular/animations';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import decode from 'jwt-decode';
import { ApiAccountService } from 'src/app/api/account/account.service';
import { ApiCustomerService } from 'src/app/api/customer/api-customer.service';
import { ApiPetService } from 'src/app/api/pet-service/pet-service.service';
import { AuthService } from 'src/app/auth/auth-service.service';
import { Account } from 'src/app/Class/Account';
import { Customer } from 'src/app/Class/Customer';
import { login } from 'src/app/Class/login';
import { Pet } from 'src/app/Class/Pet';
import { CKEditorComponent } from 'ckeditor4-angular';
import { ApiPet } from 'src/app/api/pet/api-pet.service';
import { ScheduleAvailable } from 'src/app/Class/ScheduleAvailable';
import { ServiceOption } from 'src/app/Class/PetServiceOptions';
import { petService } from 'src/app/Class/pet-service';

@Component({
  selector: 'app-questionnaire-form',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent {
  currentStep = 1;
  public acc: login;
  public isLogin = false;
  token: any;
  tokenPayload: any;
  account: Account = {};
  customer: Customer = {};

  errorAlert: string = '';
  constructor(private auth: AuthService,
    private ApiPetService:ApiPetService,
    private apiCustomer: ApiCustomerService,
    private apiAcount: ApiAccountService,
    private apiPet: ApiPet) {
    this.CheckLogin();
    this.acc = {
      username: "",
      password: ""
    };
    this.pet = {
      id: '',
      name: '',
      sex: 'đực',
      specices: 'chó',
      status: 1,
    }
  }
  //STEP 1-----------------------------------------

  CheckLogin() {
    this.token = localStorage.getItem('token');
    if (this.token == null || !this.auth.isAuthenticated()) {
      this.isLogin = false;
    }
    else {
      this.tokenPayload = decode(this.token!);
      this.account.id = this.tokenPayload.id;
      this.customer.id = this.tokenPayload.userId;
      this.apiCustomer.getById(this.customer.id!).subscribe(result => {
        this.customer = result.obj;
      })
      this.isLogin = true;
    }
  }
  login() {
    this.apiAcount.login(this.acc).subscribe(result => {
      if (result.message == 'fail') {
        if (result.details == 'Invalid account') {
          this.errorAlert = "Tài khoản hoặc mật khẩu không chính xác";
          alert(this.errorAlert);
        }
      }
      else if (result.message == 'success') {
        localStorage.setItem('token', result.token);
        this.token = result.token;
        this.tokenPayload = decode(this.token!);
        this.account.id = this.tokenPayload.id;
        this.customer.id = this.tokenPayload.userId;
        this.apiCustomer.getById(this.customer.id!).subscribe(result => {
          this.customer = result.obj;
        })
        this.isLogin = true;
      }
      else {
        console.log(result.details);
      }
    })
  }

  logout() {
    localStorage.removeItem('token');
    this.currentStep = 1;
    this.isLogin = false;
  }

  //STEP 2--------------------------------------------------------
  public config = {
    toolbar: [
      ['Bold', 'Italic', 'Underline', 'Strike'],
      ['NumberedList', 'BulletedList', 'Blockquote'],
      ['Link', 'Unlink'],
      ['Undo', 'Redo'],
    ],
  };
  petList: Pet[] = [];
  pet: Pet;
  showPetForm = false;
  selectedPetId = ''

  createNewPetForm() {
    this.pet = {
      id: '',
      name: '',
      weight: 0.1,
      birthDayFormat: '',
      sex: "đực",
      image: '',
      file: undefined,
      specices: 'chó',
      breed: '',
      healthNote: '',
      status: 1
    }
  }

  CreatePet() {
    this.apiPet.addNew(this.pet).subscribe(result => {
      if (result.status == 1) {
        alert("Thêm thành công");
        this.petList.push(result.obj);
      }
      else {
        alert("Thêm thất bại");
        console.log(result.details)
      }
    })
  }

  upload(event: Event) {
    const target = event.target as HTMLInputElement;
    const files = target.files as FileList;
    this.pet.file = files[0];
    const reader = new FileReader();
    reader.readAsDataURL(this.pet.file);
    reader.onload = () => {
      this.pet.image = reader.result as string;
    };
  }

  GetPetListByOwnerId() {
    this.apiPet.getByCustomerId(this.customer.id!).subscribe(
      result => {
        this.petList = result.list;
      }
    )
  }

  //Step 3---------------------------------------------------------
  public startedDate: Date = new Date();
  data: any[] = [
    {date: new Date('12/29/2022'), hour: '8h AM'},
    {date: new Date('12/29/2022'), hour: '9h AM'},
    {date: new Date('12/30/2022'), hour: '8h AM'},
    {date: new Date('12/31/2022'), hour: '8h AM'},
    {date: new Date('01/01/2023'), hour: '10h AM'},
    {date: new Date('01/01/2023'), hour: '11h AM'},
  ];
  scheduleAvailableList:ScheduleAvailable[]=[]
  tableHead: Date[] = [];
  services: petService[] = [];
  serviceOptions: ServiceOption[] = [];
  selectedServiceId: string ='';
  //selectedOption: ServiceOption;
  selectService() {
  }
  generateTableHead() {
    this.tableHead = []
    let currentDate = new Date(this.startedDate);
    currentDate = new Date(currentDate.setDate(currentDate.getDate() - 1));
    for (let i = 0; i < 7; i++) {
      console.log(currentDate);
      this.tableHead.push(currentDate);
      currentDate = new Date(currentDate.setDate(currentDate.getDate() + 1));
    }
  }

  submit() {

  }

  nextStep() {
    this.currentStep++;
    if (this.currentStep == 2) {
      this.GetPetListByOwnerId();
    }
  }

  previousStep() {
    this.currentStep--;
  }
  openAndClosePetForm() {
    this.showPetForm = !this.showPetForm;
  }
}