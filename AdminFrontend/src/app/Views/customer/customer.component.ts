import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Customer } from 'src/app/Class/Customer';
import { pagination } from 'src/app/Class/pagination';
import { basedSearchObject } from 'src/app/Class/SearchObjects';
import { sortingService } from 'src/app/Helper/sorting-helper';
import { ApiCustomerService } from '../customer/api-customer.service';
import { ModalCustomerComponent } from './modal-customer/modal-customer.component';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
  @ViewChild(ModalCustomerComponent) modalEmp!:ModalCustomerComponent;
  constructor(private api: ApiCustomerService,
    public http: HttpClient,
    private sort: sortingService) { }

  ngOnInit(): void {
    this.fetchData();
  }

  CustomerId: string = "";
  isSearchDivVisible:boolean = false;
  isSearching: boolean = false;
  changeExtendSearchDivVisible(){
    this.isSearchDivVisible = !this.isSearchDivVisible;
  }
  public data: Customer[] = [];
  public searchObject: basedSearchObject = {
    name: '',
    status: -1
  };

  statusMeaning: Array<string> = ["Ngừng hoạt động", "Hoạt động"];
  public page: pagination = {
    currentPage: 1,
    pageSize: 20,
    sortColumn: "firstName",
    sortOrder: "ASC",
  };

  addNew(obj: Customer) {
    this.data.push(obj);
  }
  update(obj: Customer) {
    for (let i = 0; i < this.data.length; i++) {
      if (this.data[i].id == obj.id) {
        this.data[i] = obj;
        break;
      }
    }
  }
  public totalData: number = 0;
  
  
  fetchData() {
    if (this.isSearching == true) {
      this.api.searchPage(this.page, this.searchObject).subscribe(
        result => {
         
          if (result.status == 1) {
            
            this.data = result.list;
            this.totalData = result.numberOfRecords;
          }                  
        })
    }
    else {
      this.api.getPage(this.page).subscribe((result) => {
        this.data = result.list;
        this.totalData = result.numberOfRecords;
      }
      );
    }
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
  search() {
      this.isSearching = true;
      this.page.currentPage = 1;
      this.fetchData();
  }
  resetSearching(){
    this.isSearching = false;
      this.page.currentPage = 1;
      this.fetchData();
  }
  deleteDataInBrowser(id: string) {
    for (let i = 0; i < this.data.length; i++) {
      if (this.data[i].id === id) {
        this.data.splice(i, 1);
        this.totalData -= 1;
      }
    }
  }

  openCustomerModal(modalType:string, id?:string){
    if(modalType=='create'){
      this.modalEmp.openModal(modalType);
    }
    if(modalType=='edit'){
      this.modalEmp.openModal(modalType,id);
    }
  }
}
