import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PetServiceOptionsComponent } from './pet-service-options.component';
import { ModalServiceOptionComponent } from './modal-service-option/modal-service-option.component';
import { HttpClientModule } from '@angular/common/http';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';



@NgModule({
  declarations: [  
     PetServiceOptionsComponent,
    ModalServiceOptionComponent],
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
export class ServiceOptionModule { }
