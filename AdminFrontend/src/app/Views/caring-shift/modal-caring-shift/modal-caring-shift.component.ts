import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Shift } from 'src/app/Class/Shift';
import { ApiShiftService } from '../../shift/api-shift.service';

@Component({
  selector: 'app-modal-caring-shift',
  templateUrl: './modal-caring-shift.component.html',
  styleUrls: ['./modal-caring-shift.component.css']
})
export class ModalCaringShiftComponent implements OnInit {

  constructor(private modalService: NgbModal,
    private api: ApiShiftService,
private date:DatePipe
    ) { }

  ngOnInit(): void {
   
  }


  @Input() statusMeaning;
  @Output() updateEvent = new EventEmitter();
  @Output() createEvent = new EventEmitter();

  @ViewChild('modalCaringShift') optionModal: TemplateRef<any>;
  public shift: Shift = {
    id: '',
    workingDate: null,
    startedHour:null,
    endedHour:null,
    pet:null,
    status: 0,
    createdDate: null,
    updateDate: null,
    
  };
  public typeModal = '';

  public formGroup = new FormGroup({
    WorkingDateFormat: new FormControl(this.shift.WorkingDateFormat, Validators.required),
    endedHour: new FormControl(this.shift.endedHour, [Validators.required]),
    startedHour: new FormControl(this.shift.startedHour, Validators.required)
  });

  WorkingDateFormat = this.formGroup.get('WorkingDateFormat');
  endedHour = this.formGroup.get('endedHour');
  startedHour = this.formGroup.get('startedHour');
 


  public async update(){
    
    
      this.api.update(this.shift).subscribe(result => {
        if (result.status == 0) {
          alert("Sửa thất bại")
        }
        if (result.status == 1) {
          alert("Sửa thành công")
          this.shift = result.obj;
          this.updateEvent.emit(this.shift);
        }
      })
    
    
  }

  public openModal(typeModal: string, shift: Shift) {
    this.typeModal = typeModal;
    this.shift = shift;
    this.shift.WorkingDateFormat = this.date.transform(this.shift.workingDate, 'dd-MM-yyyy');
    this.modalService.open(this.optionModal);
   
  }
}
