import { Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SellBill } from 'src/app/Class/SellBill';
import { ApiServiceSellBillService } from '../api-service-sell-bill.service';

@Component({
  selector: 'app-modal-service-sell-bill',
  templateUrl: './modal-service-sell-bill.component.html',
  styleUrls: ['./modal-service-sell-bill.component.css']
})
export class ModalServiceSellBillComponent implements OnInit {
  constructor(private modalService: NgbModal,
    private api: ApiServiceSellBillService,

    ) { }

  ngOnInit(): void {
   
  }


  @Input() statusMeaning;
  @Output() updateEvent = new EventEmitter();
  @Output() createEvent = new EventEmitter();

  @ViewChild('modalSellBill') optionModal: TemplateRef<any>;
  public option: SellBill = {
    id: '',
    employeeAccountId:'',
    customerAccountId:'',
    status: 0,
    createdDate: null,
    updateDate: null,
    
  };
  public typeModal = '';


  public createNew() {
    this.option = {
      id: '',
      employeeAccountId:'',
      customerAccountId:'',
      status: 0,
      createdDate: null,
      updateDate: null,
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

  public async update(){
    
    
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
