import { Component, OnInit , OnDestroy,ChangeDetectorRef, ViewChild } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { io, Socket } from 'socket.io-client';
import { CountStatus } from 'src/app/Class/CountStatus';
import { pagination } from 'src/app/Class/pagination';
import { basedSearchObject } from 'src/app/Class/SearchObjects';
import { SellBill } from 'src/app/Class/SellBill';
import { sortingService } from 'src/app/Helper/sorting-helper';
import { ApiServiceSellBillService } from './api-service-sell-bill.service';
import { ModalServiceSellBillComponent } from './modal-service-sell-bill/modal-service-sell-bill.component';

@Component({
  selector: 'app-service-sell-bill',
  templateUrl: './service-sell-bill.component.html',
  styleUrls: ['./service-sell-bill.component.css']
})
export class ServiceSellBillComponent implements OnInit,OnDestroy {

  public searchObject: basedSearchObject = {
    status: 0,
    startDate:'0001-01-01',
    endDate: '9999-01-01',
    phoneNumber:null
  };

  public page: pagination = {
    currentPage: 1,
    pageSize: 20,
    sortColumn: "totalPaid",
    sortOrder: "ASC",
  };
  statusMeaning: Array<string> = ["Chưa xử lý", "Đang xử lý","Chưa thanh toán", "Đã thanh toán"];
  public CountStatus: CountStatus[] = [];
  public data:SellBill[]=[];
  public totalData = 0;
  isSearchDivVisible: boolean = false;
  isSearching: boolean = false;
  constructor(public apiSellService: ApiServiceSellBillService, private sort: sortingService, private changeRef:ChangeDetectorRef) {
    this.startConnection();
    setTimeout(() => {
      this.addStatusListListener();
      this.addPageDataListener();
      this.addAddSellBillListener();
      this.getAllStatus();
      this.getPageByStatus();
     
    }, 5000);

   
   }

  ngOnInit() {
    
  }
  @ViewChild(ModalServiceSellBillComponent) modalSellBill!:ModalServiceSellBillComponent;
  private hubConnection: signalR.HubConnection
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7218/sellbill', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })  
     .withAutomaticReconnect()
      .build();
    this.hubConnection.keepAliveIntervalInMilliseconds = 30000000;
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public addStatusListListener = () => {
    this.hubConnection.on('statusList', (data) => {
      this.CountStatus = data.statusList;
    });
  }
  public addAddSellBillListener = () => {
    this.hubConnection.on('addSellBill', (data) => {
      let sellBill:SellBill = data.obj;
      if(this.searchObject.status = sellBill.status){
        this.data.push(sellBill);
      }
      this.CountStatus[0].quantityOfStatus+=1;
      this.totalData +=1;
     
      console.log(data.obj);
      this.changeRef.detectChanges();
    });
  }

  public addPageDataListener = () => {
    this.hubConnection.on('pageData', (data) => {
      this.data = data.list;
      this.totalData = data.numberOfRecords;
      console.log(this.data);
    });
  }
  getPageByStatus() {
    this.apiSellService.getPage(this.page, this.searchObject).subscribe((data:any) => {
        console.log(data);
    });
  }

  getAllStatus() {
    this.apiSellService.GetAllStatus().subscribe((data:any) => {
      console.log(data);
    });
  }
  renderPage(event: number) {
    this.page.currentPage = event;
    this.getPageByStatus();
  }

  GetSortColumn(column: string) {
    if (this.page.sortColumn != column) {
      this.page.sortOrder = '';
    }
    this.page.sortColumn = column;
    this.page.sortOrder = this.sort.changeSortOrder(this.page.sortOrder);
    this.getPageByStatus();
  }

  addNew(option: any) {
    this.data.push(option);
  }
  update(option: any) {
    for (let i = 0; i < this.data.length; i++) {
      if (this.data[i].id == option.id && option.status == this.searchObject.status) {
        this.data[i] = option;
        break;
      }
    }
    this.getAllStatus();
  }
  
  delete(id: string) {
    const cf = confirm("Bạn có chắc chắn muốn xóa không?");
    if (cf) {
      this.apiSellService.delete(id).subscribe(result => {
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
    this.getPageByStatus();
  }

  resetSearching() {
    this.isSearching = false;
    this.page.currentPage = 1;
    this.searchObject = {
      status: 0,
      startDate:'0001-01-01',
      endDate: '9999-01-01',
      phoneNumber:null
    }
    this.getPageByStatus();

  }
  changeStatus(n:number){
    this.searchObject.status = n;
    this.getPageByStatus();
  }
  openModal(typeModal:string,id?:string){
   if(typeModal == 'create'){
    this.modalSellBill.openModal(typeModal);
   }
   else{
    this.modalSellBill.openModal(typeModal,id);
   }
  }
  ngOnDestroy(){
    this.apiSellService.destroy();
  }
}
