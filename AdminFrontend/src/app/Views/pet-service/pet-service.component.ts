import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { PetServiceApiService } from './pet-service-api.service';
import { sortingService } from 'src/app/Helper/sorting-helper';
import { pagination } from 'src/app/Class/pagination';
import { petService } from 'src/app/Class/pet-service';
import { Product } from 'src/app/Class/product';
import { ProductComponent } from '../product/product.component';
import { ServiceOptionsComponent } from './service-options/service-options.component';
import { CreateModalPetServiceComponent } from './create-modal-pet-service/create-modal-pet-service.component';
import { ModalServiceImageManagementComponent } from './modal-service-image-management/modal-service-image-management.component';
@Component({
  selector: 'app-pet-service',
  templateUrl: './pet-service.component.html',
  styleUrls: ['./pet-service.component.css']
})
export class PetServiceComponent implements OnInit  {
  @ViewChild(CreateModalPetServiceComponent) createForm!: CreateModalPetServiceComponent;
  @ViewChild(ServiceOptionsComponent) option!: ServiceOptionsComponent;
  @ViewChild(ModalServiceImageManagementComponent) imageModal!: ModalServiceImageManagementComponent;

  PetServiceId:string ="";
  public data:petService[] = [];
  statusMeaning:Array<string>=["Không hoạt động", "Đang hoạt động"];
  public page:pagination ={
    currentPage: 1,
    pageSize:20,
    sortColumn: "name",
    sortOrder: "ASC"
  };
 
  addNew(service:petService){
    this.data.push(service);
  }
  public totalData:number = 0;
  constructor(private api:PetServiceApiService,
    public http: HttpClient,
    private sort: sortingService) { }

  ngOnInit(): void {
    this.fetchData();
  }
  openCreateForm(){
    this.createForm.openModal();
  }
  OpenOption(){
    this.option.openModal();
  }
  openModalImage(id:string){
    this.imageModal.openModal(id);
  }
  fetchData() {
    this.api.getPage(this.page).subscribe((result)=>{
       this.data = result.list;
       this.totalData = result.numberOfRecords;} 
   );
  }

  renderPage(event: number) {
    this.page.currentPage = event;
    this.fetchData();
  }
  
  GetSortColumn(column: string){
    if(this.page.sortColumn != column){
      this.page.sortOrder = '';
    }
    this.page.sortColumn = column;
    this.page.sortOrder = this.sort.changeSortOrder(this.page.sortOrder);
    this.fetchData();
  }
}
