import { Guid } from 'guid-typescript';
import { animate, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
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
import { ApiServiceOption } from 'src/app/api/service-option/service-option.service';
import { ServiceSellPrice } from 'src/app/Class/ServiceSellPrice';
import { ApiServicePrice } from 'src/app/api/service-price/service-price.service';
import { ApiScheduleAvailableService } from 'src/app/api/schedule-available/api-schedule-available.service';
import { DatePipe } from '@angular/common';
import { Schedule } from 'src/app/Class/Schedule';
import { TableSchedule } from 'src/app/Class/table-schedule';
import { SellBill } from 'src/app/Class/SellBill';
import { ApiScheduleService } from 'src/app/api/schedule/api-schedule.service';
import { ApiSellbillService } from 'src/app/api/sellbill/api-sellbill.service';
@Component({
  selector: 'app-questionnaire-form',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent implements OnInit {
  currentStep = 1;
  public acc: login;
  public isLogin = false;
  token: any;
  tokenPayload: any;
  account: Account = {};
  customer: Customer = {};

  errorAlert: string = '';
  constructor(private auth: AuthService,
    private ApiPetService: ApiPetService,
    private apiCustomer: ApiCustomerService,
    private apiAcount: ApiAccountService,
    private apiPetServiceOption: ApiServiceOption,
    private apiServicePrice: ApiServicePrice,
    private apiScheduleAvailable: ApiScheduleAvailableService,
    private apiPet: ApiPet,
    private apiSchedule:ApiScheduleService,
    private apiSellBill:ApiSellbillService,
    private datePipe: DatePipe) {
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
  ngOnInit(): void {
    this.getAllPetService();
    this.startedDateFormat = '';
    this.bookingDateList = [];
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
      this.sellBill.customerAccountId = this.tokenPayload.id;
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
  selectedPet: Pet = {
    id: '',
    name: '',
    sex: ''
  }
  selectPet(pet: Pet) {
    this.selectedPet = pet;
    this.getAllPetServiceOption();
  }
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
  tableDateData: TableSchedule[] = [];
  scheduleAvailableList: ScheduleAvailable[] = [];
  tableHead: Date[] = [];
  services: petService[] = [];
  serviceOptionsList: ServiceOption[] = [];
  displayServiceOptions: ServiceOption[] = [];
  servicePrice: ServiceSellPrice[] = [];
  bookingDateList: Schedule[] = [];
  selectedServiceId: string = '';
  selectedOption: ServiceOption = {
    id: '',
    name: '',
    petServiceId: '',
  };
  minStartedDate = this.datePipe.transform(new Date(), 'yyyy-MM-dd');
  public startedDateFormat: string = this.datePipe.transform(new Date(), "yyyy-MM-dd") as string;
  getAllPetService() {
    this.ApiPetService.getAllPetService().subscribe(result => {
      this.services = result.list;
    })
  }

  async getAllPetServiceOption() {
    this.apiPetServiceOption.getAllPetServiceOption(this.selectedPet.weight!).subscribe(result => {
      this.serviceOptionsList = result.list;
    })
  }
  getAllServicePrice() {
    this.apiServicePrice.getAllPrice().subscribe(result => {
      this.servicePrice = result.list;
    })
  }
  getScheduleData() {
    this.tableDateData = [];
    this.generateTableHead();
    this.startedDateFormat = this.datePipe.transform(this.startedDateFormat!, "yyyy-MM-dd") as string;
    this.apiScheduleAvailable.getScheduleByOptionId(this.selectedOption.id!, this.startedDateFormat!).subscribe(result => {
      this.scheduleAvailableList = result.list;
      for (let i = 0; i < this.tableHead.length; i++) {
        for (let j = 0; j < this.scheduleAvailableList.length; j++) {
          let startedDate = new Date(this.scheduleAvailableList[j].startedDate!).getTime();
          let endedDate = new Date(this.scheduleAvailableList[j].endedDate!).getTime();
          let tableHeadDate = new Date(this.tableHead[i]).getTime();
          if (tableHeadDate >= startedDate && tableHeadDate <= endedDate) {
            let dateData: TableSchedule = {
              id:Guid.create().toString(),
              selected: false,
              scheduleDate: this.tableHead[i],
              scheduleHour: this.scheduleAvailableList[j].availableHour!
            }
            this.tableDateData.push(dateData);
          }
        }
      }
    });
      console.log(this.tableDateData);
  }

  selectService() {
    this.displayServiceOptions = this.serviceOptionsList.filter(x => x.petServiceId === this.selectedServiceId);
  }

  selectOption(option: ServiceOption) {
    this.selectedOption = option;
  }

  generateTableHead() {
    this.tableHead = []
    let currentDate = new Date(this.startedDateFormat);
    currentDate = new Date(currentDate.setDate(currentDate.getDate() - 1));
    for (let i = 0; i < 7; i++) {
      this.tableHead.push(currentDate);
      currentDate = new Date(currentDate.setDate(currentDate.getDate() + 1));
    }

  }

  gethour(date: Date) {
    let result = this.tableDateData.filter(x => x.scheduleDate.toDateString() === date.toDateString())
    return result;
  }
  addSchedule(dataTableSchedule:TableSchedule) {
    let booking: Schedule = {
      id: Guid.create().toString(),
      bookingDate: dataTableSchedule.scheduleDate,
      bookingHour: dataTableSchedule.scheduleHour,
      grandPaid: this.selectedOption.price!,
      status: 0,
      serviceOptionId: this.selectedOption.id,
      serviceOptionName:this.selectedOption.name,
      sellBillId: '',
      petId: this.selectedPet.id,
      selectedTableId: dataTableSchedule.id
    }
  
    this.bookingDateList.push(booking);
    for(let i = 0;i<this.tableDateData.length;i++){
      if(this.tableDateData[i].id == dataTableSchedule.id){
        this.tableDateData[i].selected = true;
      }
    }
    this.calculateTotalPaid();
  }


  removeBooking(id:string){
    let tableSelectedId = '';
    let conf = confirm("Bạn có chắc chắn muốn xóa?")
    if(conf){
      for(let i = 0; i<this.bookingDateList.length;i++){
        if(this.bookingDateList[i].id == id){
          tableSelectedId = this.bookingDateList[i].selectedTableId!;
          this.bookingDateList.splice(i, 1);
        }
      }
      for(let i = 0;i<this.tableDateData.length;i++){
        if(this.tableDateData[i].id == tableSelectedId){
          this.tableDateData[i].selected = false;
        }
      }
    }
  }

  //Step 4 -------------------------------------------------------------
  sellBill:SellBill= {
    id:'',
    customerAccountId:'',
    employeeAccountId:'1',
    billType: 'service',
    status: 0
  };


  calculateTotalPaid(){
    this.sellBill.totalPaid = 0;
    for(let i = 0; i<this.bookingDateList.length;i++){
      this.sellBill.totalPaid +=this.bookingDateList[i].grandPaid!;
    }
  }

  addSellBill(){
    this.apiSellBill.addNew(this.sellBill).subscribe(result=>{
      this.sellBill = result.obj;
      for(let i = 0;i<this.bookingDateList.length;i++){
        this.bookingDateList[i].sellBillId = this.sellBill.id;
      }
      this.apiSchedule.addNew(this.bookingDateList).subscribe(result=>{
        if(result.status == 1){
          alert("Đặt đơn thành công");
        }
        else{
          alert("Đặt hàng thất bại");
          console.log(result.details);
        }
      })
    })
  }
  submit() {
    this.addSellBill();
  }

  nextStep() {
    this.currentStep++;
    if (this.currentStep == 2) {
      this.GetPetListByOwnerId();
    }
    console.log(this.customer);
    console.log(this.account);
    console.log(this.sellBill);

  }

  previousStep() {
   
    this.currentStep--;
  }
  openAndClosePetForm() {
    this.showPetForm = !this.showPetForm;
  }
}