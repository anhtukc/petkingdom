import { Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { validateHelper } from 'src/app/Helper/validate-helper';
import { ApiServiceOptions } from '../api-service-options.service';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ScheduleAvailable } from 'src/app/Class/ScheduleAvailable';
import { ApiScheduleAvailableService } from './api-schedule-available.service';
import { pagination } from 'src/app/Class/pagination';
import { sortingService } from 'src/app/Helper/sorting-helper';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-schedule-available',
  templateUrl: './schedule-available.component.html',
  styleUrls: ['./schedule-available.component.css']
})
export class ScheduleAvailableComponent implements OnInit {
  formattedDate: string='';
  constructor(private modalService: NgbModal,
    private api: ApiScheduleAvailableService,
    private sort: sortingService,
    private datePipe:DatePipe) { }
  ngOnInit(): void {
    this.currentTimestamp = Date.now();
    this.createNew();
  }
  public modalType=''
  currentTimestamp = Date.now();
  public page: pagination = {
    currentPage: 1,
    pageSize: 20,
    sortColumn: "availableHour",
    sortOrder: "ASC",
  };
  @Input() serviceOptionId:string
  public totalData = 0;
  public Editor = ClassicEditor;
  @Input() statusMeaning;
  data: ScheduleAvailable[] = [];
  @ViewChild('modalScheduleAvailable') optionModal: TemplateRef<any>;
  public schedule: ScheduleAvailable = {
    id: '',
    startedDate: new Date(this.currentTimestamp),
    endedDate: new Date(this.currentTimestamp),
    availableHour: null,
    status: 1,
    serviceOptionId: '',
    createdDate: null,
    updateDate: null,
    startedDateFormat:'2022-12-12',
      endedDateFormat:'2022-12-12'
  };
  public formGroup = new FormGroup({
    startedDate: new FormControl(this.schedule.startedDate, Validators.required),
    endedDate: new FormControl(this.schedule.endedDate, [Validators.required, this.endDateValidator.bind(this)]),
    availableHour: new FormControl(this.schedule.availableHour, Validators.required)
  });

  endDateValidator(control: FormControl): { [key: string]: boolean } | null {
    if (control.value && control.value < this.schedule.startedDate) {
      return { 'endDateTooEarly': true };
    }
    return null;
  }
  startedDate = this.formGroup.get('startedDate');
  endedDate = this.formGroup.get('endedDate');
  availableHour = this.formGroup.get('availableHour');

  public createNew() {
    this.schedule = {
      id: '',
      startedDate: null,
      endedDate: null,
      availableHour: null,
      status: 1,
      serviceOptionId: this.serviceOptionId,
      createdDate: null,
      updateDate: null,
      startedDateFormat:null,
      endedDateFormat:null
    };
    Object.keys(this.formGroup.controls).forEach(key => {
      this.formGroup.controls[key].clearValidators();
    });
    this.modalType ='create';
  
  }
  
  public getById(id: string) {
     this.modalType='edit';
    for(let i = 0;i < this.data.length;i++){
      if(this.data[i].id == id){
        this.schedule = this.data[i];
        this.schedule.startedDateFormat = this.datePipe.transform(this.schedule.startedDate, 'yyyy-MM-dd');
        this.schedule.endedDateFormat = this.datePipe.transform(this.schedule.startedDate, 'yyyy-MM-dd');
        break;
      }
    }
  }

 
  public async create() {

    if (this.formGroup.valid) {
      this.api.addNew(this.schedule).subscribe(result => {
        if (result.status == 0) {
          alert("Thêm mới thất bại")
        }
        if (result.status == 1) {
          alert("Thêm mới thành công")
          this.schedule = result.obj;
          this.data.push(this.schedule);
          this.createNew();
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
  delete(id: string) {
    const cf = confirm("Bạn có chắc chắn muốn xóa không?");
    if (cf) {
      this.api.delete(id).subscribe(result => {
        if (result.status == 0) {
          alert("Không tìm được đối tượng");
          console.log(result.details);
        }
        if (result.status == 1) {
          alert("Xóa thành công");
          this.deleteDataInBrowser(id);
        }
      })
    }
  }

  deleteDataInBrowser(id: string) {
    for (let i = 0; i < this.data.length; i++) {
      if (this.data[i].id === id) {
        this.data.splice(i, 1);
        this.totalData -= 1;
      }
    }
  }
  public async update() {
    if (this.formGroup.valid) {
      this.api.update(this.schedule).subscribe(result => {
        if (result.status == 0) {
          alert("Sửa thất bại")
        }
        if (result.status == 1) {
          alert("Sửa thành công")
          this.schedule = result.obj;
          for(let i = 0;i<this.data.length;i++){
            if(this.data[i].id == this.schedule.id){
              this.data[i] = this.schedule;
            }
          }
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

  fetchData(){
    this.api.getPage(this.page, this.serviceOptionId).subscribe(result => {
      this.data = result.list;
      this.totalData = result.numberOfRecords;
    })
    console.log(this.serviceOptionId);
  }

  public openModal(id:string) {
    this.modalType='create';
    this.serviceOptionId =id;
    this.fetchData();
    this.modalService.open(this.optionModal);
   
  }

  renderPage(event: number) {
    this.page.currentPage = event;
    this.fetchData();
  }

  GetSortColumn(column: string) {
    if (this.page.sortColumn != column) {
      this.page.sortOrder = '';
    }
    this.page.sortColumn = column;
    this.page.sortOrder = this.sort.changeSortOrder(this.page.sortOrder);
    this.fetchData();
  }

}
