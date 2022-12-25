import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { PetServiceOptionsComponent } from './pet-service-options.component';
import { ModalServiceOptionComponent } from './modal-service-option/modal-service-option.component';
import { HttpClientModule } from '@angular/common/http';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { ScheduleAvailableComponent } from './schedule-available/schedule-available.component';



@NgModule({
  declarations: [  
     PetServiceOptionsComponent,
    ModalServiceOptionComponent,
    ScheduleAvailableComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    NgxPaginationModule,
    FormsModule,
    BrowserAnimationsModule,
    NgbModule,
    ReactiveFormsModule,
    CKEditorModule
  ],
  providers: [
    DatePipe
  ]
})
export class ServiceOptionModule { }
