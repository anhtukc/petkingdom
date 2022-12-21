import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { PetServiceApiService } from './pet-service-api.service';
import { sortingService } from 'src/app/Helper/sorting-helper';
import { pagination } from 'src/app/Class/pagination';
import { petService } from 'src/app/Class/pet-service';
import { CreateModalPetServiceComponent } from './create-modal-pet-service/create-modal-pet-service.component';
import { ModalServiceImageManagementComponent } from './modal-service-image-management/modal-service-image-management.component';
import { ModalUpdatePetServiceComponent } from './modal-update-pet-service/modal-update-pet-service.component';
@Component({
  selector: 'app-pet-service',
  templateUrl: './pet-service.component.html',
  styleUrls: ['./pet-service.component.css']
})
export class PetServiceComponent implements OnInit  {
  @ViewChild(CreateModalPetServiceComponent) createForm!: CreateModalPetServiceComponent;
  @ViewChild(ModalServiceImageManagementComponent) imageModal!: ModalServiceImageManagementComponent;
  @ViewChild(ModalUpdatePetServiceComponent) updateForm!: ModalUpdatePetServiceComponent;
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
  update(service:petService){
    for(let i = 0;i<this.data.length;i++){
      if(this.data[i].id == service.id){
        this.data[i] = service;
        break;
      }
    }
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
  openUpdateForm(id:string){
    this.updateForm.openModal(id);
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

  delete(id:string){
    const cf = confirm("Bạn có chắc chắn muốn xóa không?");
    if(cf){
      this.api.delete(id).subscribe(result=>{
        if(result.message == "fail"){
          alert("Không tìm được đối tượng");
          console.log(result.details);
        }
        if(result.message == "success"){
          alert("Xóa thành công");    
          this.deleteDataInBrowser(id);     
        }
      })
    }
  }
  deleteDataInBrowser(id:string){
    for(let i = 0; i<this.data.length;i++){
      if(this.data[i].id === id){
        this.data.splice(i,1);
        this.totalData -=1;
      }
    }
  }
}
