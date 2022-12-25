import { Component, OnInit, ViewChild } from '@angular/core';
import { pagination } from 'src/app/Class/pagination';
import { petService } from 'src/app/Class/pet-service';
import { ServiceOption } from 'src/app/Class/PetServiceOptions';
import { basedSearchObject } from 'src/app/Class/SearchObjects';
import { sortingService } from 'src/app/Helper/sorting-helper';
import { ApiPetService } from '../pet-service/pet-service-api.service';
import { ApiServiceOptions } from './api-service-options.service';
import { ModalServiceOptionComponent } from './modal-service-option/modal-service-option.component';
import { ScheduleAvailableComponent } from './schedule-available/schedule-available.component';

@Component({
  selector: 'app-pet-service-options',
  templateUrl: './pet-service-options.component.html',
  styleUrls: ['./pet-service-options.component.css']
})
export class PetServiceOptionsComponent implements OnInit {
  constructor(private api: ApiServiceOptions,private petServiceApi:ApiPetService,
    private sort: sortingService) { }

  ngOnInit(): void {
    this.petServiceApi.getAll().subscribe(result=>{
      this.petService = result.list;
      
    })
    this.fetchData();
 
  }
  public serviceOptionId ='';
  statusMeaning: Array<string> = ["Không hoạt động", "Đang hoạt động"];
  public page: pagination = {
    currentPage: 1,
    pageSize: 20,
    sortColumn: "name",
    sortOrder: "ASC"
  };
  public totalData = 0;
  public data: ServiceOption[] = [];
  public petService: petService[] = [];
  public searchObject: basedSearchObject = {
    name: '',
    status: -1
  };
  @ViewChild(ModalServiceOptionComponent) modalServiceOption!:ModalServiceOptionComponent;
  @ViewChild(ScheduleAvailableComponent) modalScheduleAvailable!:ScheduleAvailableComponent;

  isSearchDivVisible: boolean = false;
  isSearching: boolean = false;

 
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

  addNew(option: ServiceOption) {
    this.data.push(option);
  }
  update(option: ServiceOption) {
    for (let i = 0; i < this.data.length; i++) {
      if (this.data[i].id == option.id) {
        this.data[i] = option;
        break;
      }
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


  changeExtendSearchDivVisible() {
    this.isSearchDivVisible = !this.isSearchDivVisible;
  }

  search() {
    this.isSearching = true;
    this.page.currentPage = 1;
    this.fetchData();
  }

  resetSearching() {
    this.isSearching = false;
    this.page.currentPage = 1;
    this.fetchData();
  }

  openModal(typeModal:string,id?:string){
    if(typeModal=='create'){
      this.modalServiceOption.openModal(typeModal);
    }
    if(typeModal=='edit'){
      this.modalServiceOption.openModal(typeModal,id);
    }
  }
  
  openScheduleModal(id:string){
    this.serviceOptionId = id;
    this.modalScheduleAvailable.openModal(id);
  
  }
}
