import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ApiAccountService } from 'src/app/api/account/api-account.service';
import { Account } from 'src/app/Class/Account';
import { Schedule } from 'src/app/Class/Schedule';
import { Shift } from 'src/app/Class/Shift';
import { ApiEmployee } from '../../employee/api-employee.service';
import { ApiShiftService } from '../api-shift.service';

@Component({
  selector: 'app-modal-shift',
  templateUrl: './modal-shift.component.html',
  styleUrls: ['./modal-shift.component.css']
})
export class ModalShiftComponent implements OnInit {
  @Input() statusMeaning;
  @Output() updateEvent = new EventEmitter();
  @Output() createEvent = new EventEmitter();

  caringStaffList:Account[] = []
  @ViewChild('modalShift') modalShift: TemplateRef<any>;
  public shift: Shift = {
    id: '',
    workingDate:null,
    startedHour:null,
    endedHour:null,
    status: 1,
    createdDate: null,
    updateDate: null,
  };
  public formGroup = new FormGroup({
    WorkingDateFormat: new FormControl(this.shift.WorkingDateFormat, Validators.required),
    endedHour: new FormControl(this.shift.endedHour, [Validators.required]),
    startedHour: new FormControl(this.shift.startedHour, Validators.required)
  });

  WorkingDateFormat = this.formGroup.get('WorkingDateFormat');
  endedHour = this.formGroup.get('endedHour');
  startedHour = this.formGroup.get('startedHour');
  typeModal: string;
  public createNew() {
    this.shift = {
      id: '',
      workingDate:null,
      startedHour:null,
      endedHour:null,
      status: 1,
      createdDate: null,
      updateDate: null,
      WorkingDateFormat: this.datePipe.transform(new Date(), "yyyy-MM-dd")
    };
  }

  // public getById(id: string) {
  //   this.apiShift.getById(id).subscribe(
  //     result => {
  //       this.shift = result.obj;
  //     }
  //   )
  // }

  public async create() {
    
    if (this.formGroup.valid) {
      this.apiShift.addNew(this.shift).subscribe(result => {
        if (result.status == 0) {
          alert("Thêm mới thất bại")
        }
        if (result.status == 1) {
          alert("Thêm mới thành công")
          this.shift = result.obj;
          this.createEvent.emit(this.shift);
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

  // public async update(){
  //   this.getBlogCategory();
  //   if (this.formGroup.valid) {
  //     this.apiShift.update(this.blog).subscribe(result => {
  //       if (result.status == 0) {
  //         alert("Sửa thất bại")
  //       }
  //       if (result.status == 1) {
  //         alert("Sửa thành công")
  //         this.blog = result.obj;
  //         this.updateEvent.emit(this.blog);
  //       }
  //     })
  //   }
  //   else {
  //     Object.keys(this.formGroup.controls).forEach(key => {
  //       this.formGroup.controls[key].markAsTouched();
  //       this.formGroup.controls[key].markAsDirty();
  //     });
  //   }
  // }

  public openModal(typeModal: string, id?: string) {
    this.typeModal = typeModal;
    this.modalService.open(this.modalShift);
  
      this.createNew();
      this.shift.scheduleId = id;
    // if (typeModal == 'edit') {
    //   this.getById(id);
    // }
  }
  constructor(private datePipe:DatePipe, private apiAccount:ApiAccountService, private apiShift:ApiShiftService,private modalService: NgbModal) {
   
   }

  ngOnInit(): void {
    this.apiAccount.getall("caringStaff").subscribe(result=>{
      this.caringStaffList = result.list;
    })
  }

}
