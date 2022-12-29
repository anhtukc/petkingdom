import { DatePipe } from '@angular/common';
import { Component, Input, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { pagination } from 'src/app/Class/pagination';
import { sortingService } from 'src/app/Helper/sorting-helper';
import { ApiServiceOptionPrice } from './api-service-option-price.service';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ServiceSellPrice } from 'src/app/Class/ServiceSellPrice';
@Component({
  selector: 'app-service-option-price',
  templateUrl: './service-option-price.component.html',
  styleUrls: ['./service-option-price.component.css']
})
export class ServiceOptionPriceComponent implements OnInit {
  public modalType = ''
  constructor(private modalService: NgbModal,
    private api: ApiServiceOptionPrice,
    private sort: sortingService,
    private datePipe: DatePipe) { }
  ngOnInit(): void {
    this.createNew();
  }

  public page: pagination = {
    currentPage: 1,
    pageSize: 20,
    sortColumn: "unitPrice",
    sortOrder: "desc",
  };
  @Input() serviceOptionId: string
  public totalData = 0;
  public Editor = ClassicEditor;
  @Input() statusMeaning;
  data: ServiceSellPrice[] = [];
  @ViewChild('modalServiceSellPrice') optionModal: TemplateRef<any>;
  public price: ServiceSellPrice = {
    id: '',
    unitPrice: 0,
    petMinimumWeight: 0.1,
    petMaximumWeight: 1,
    status: 1,
    serviceOptionId: '',
    createdDate: null,
    updateDate: null
  };
  public formGroup = new FormGroup({
    unitPrice: new FormControl(this.price.unitPrice,
      [
        Validators.required,
        Validators.min(10000)
    ]),
    petMinimumWeight: new FormControl(this.price.petMinimumWeight,
      [ 
        Validators.pattern("^[0-9]*(\.?[0-9]{1})?$"),
        Validators.required,
        Validators.min(0.1)
      ]),
      petMaximumWeight: new FormControl(this.price.petMaximumWeight, 
        [
          Validators.pattern("^[0-9]*(\.?[0-9]{1})?$"),
          Validators.required,
          this.petMaximumWeightValidator.bind(this)]
        )
  });

  petMaximumWeightValidator(control: FormControl): { [key: string]: boolean } | null {
    if (control.value && control.value < this.price.petMinimumWeight) {
      return { 'mustBigger': true };
    }
    return null;
  }
  unitPrice = this.formGroup.get('unitPrice');
  petMinimumWeight = this.formGroup.get('petMinimumWeight');
  petMaximumWeight = this.formGroup.get('petMaximumWeight');

  public createNew() {
    this.price = {
      id: '',
      unitPrice: 10000,
      petMinimumWeight: 0.1,
      petMaximumWeight: 1,
      status: 1,
      serviceOptionId: this.serviceOptionId,
      createdDate: null,
      updateDate: null
    };
    Object.keys(this.formGroup.controls).forEach(key => {
      this.formGroup.controls[key].clearValidators();
    });
    this.modalType = 'create';

  }

  public getById(id: string) {
    this.modalType = 'edit';
    for (let i = 0; i < this.data.length; i++) {
      if (this.data[i].id == id) {
        this.price = this.data[i];
        break;
      }
    }
  }


  public async create() {

    if (this.formGroup.valid) {
      this.api.addNew(this.price).subscribe(result => {
        if (result.status == 0) {
          alert("Thêm mới thất bại")
        }
        if (result.status == 1) {
          alert("Thêm mới thành công")
          this.price = result.obj;
          this.data.push(this.price);
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
      this.api.update(this.price).subscribe(result => {
        if (result.status == 0) {
          alert("Sửa thất bại")
        }
        if (result.status == 1) {
          alert("Sửa thành công")
          this.price = result.obj;
          for (let i = 0; i < this.data.length; i++) {
            if (this.data[i].id == this.price.id) {
              this.data[i] = this.price;
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

  fetchData() {
    this.api.getPage(this.page, this.serviceOptionId).subscribe(result => {
      this.data = result.list;
      this.totalData = result.numberOfRecords;
    })
    console.log(this.serviceOptionId);
  }

  public openModal(id: string) {
    this.modalType = 'create';
    this.serviceOptionId = id;
    this.price.serviceOptionId = id;
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
