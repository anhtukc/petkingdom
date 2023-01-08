import { Component, OnInit, ViewChild } from '@angular/core';
import { pagination } from 'src/app/Class/pagination';
import { Schedule } from 'src/app/Class/Schedule';
import { basedSearchObject } from 'src/app/Class/SearchObjects';
import { Shift } from 'src/app/Class/Shift';
import { sortingService } from 'src/app/Helper/sorting-helper';
import { ApiScheduleService } from './api-schedule.service';
import { ApiShiftService } from './api-shift.service';
import { ModalShiftComponent } from './modal-shift/modal-shift.component';

@Component({
  selector: 'app-shift',
  templateUrl: './shift.component.html',
  styleUrls: ['./shift.component.css']
})
export class ShiftComponent implements OnInit {
  @ViewChild(ModalShiftComponent) modalShift!:ModalShiftComponent;
  constructor(private api: ApiShiftService,
    private apiSchedule:ApiScheduleService,
    private sort: sortingService) { }

  ngOnInit(): void {
    this.fetchData();
  }


  isSearchDivVisible:boolean = false;
  isSearching: boolean = false;
  changeExtendSearchDivVisible(){
    this.isSearchDivVisible = !this.isSearchDivVisible;
  }
  public data: Schedule[] = [];
  public searchObject: basedSearchObject = {
    name: '',
    status: -1
  };

  statusMeaning: Array<string> = ["Chưa khởi tạo","Đã khởi tạo", "Đang thực hiện", "Đã hoàn thành"];
  public page: pagination = {
    currentPage: 1,
    pageSize: 20,
    sortColumn: "updateDate",
    sortOrder: "ASC",
  };


  addNew(shift: Shift) {
    for (let i = 0; i < this.data.length; i++) {
      if (this.data[i].id == shift.id) {
        this.data[i].status = 1;
        break;
      }
    }
  }
  public totalData: number = 0;
  
  
  fetchData() {
    if (this.isSearching == true) {
      this.apiSchedule.searchPage(this.page, this.searchObject).subscribe(
        result => {
         
          if (result.status == 1) {
            
            this.data = result.list;
            this.totalData = result.numberOfRecords;
          }                  
        })
    }
    else {
      this.apiSchedule.getPage(this.page).subscribe((result) => {
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
      this.apiSchedule.delete(id).subscribe(result => {
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

  openEmployeeModal(modalType:string, id?:string){
    if(modalType=='create'){
      this.modalShift.openModal(modalType);
    }
    if(modalType=='edit'){
      this.modalShift.openModal(modalType,id);
    }
  }

}
