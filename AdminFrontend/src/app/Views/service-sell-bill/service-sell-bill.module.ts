import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ServiceSellBillComponent } from './service-sell-bill.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { ModalServiceSellBillComponent } from './modal-service-sell-bill/modal-service-sell-bill.component';



@NgModule({
  declarations: [
    ServiceSellBillComponent,
    ModalServiceSellBillComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    NgxPaginationModule,
    FormsModule,
    BrowserAnimationsModule,
    NgbModule,
    ReactiveFormsModule,
    CKEditorModule
  ]
})
export class ServiceSellBillModule { }
