import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerComponent } from './customer.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { ModalCustomerComponent } from './modal-customer/modal-customer.component';


@NgModule({
  declarations: 
  [
    ModalCustomerComponent,
    CustomerComponent,
  ],
  imports: [
    CommonModule   
    ,HttpClientModule,
    NgxPaginationModule,
    FormsModule,
    BrowserAnimationsModule,
    NgbModule,
    ReactiveFormsModule,
    CKEditorModule,

  ],

})
export class CustomerModule { }
