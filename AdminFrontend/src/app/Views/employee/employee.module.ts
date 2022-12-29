import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalEmployeeComponent } from './modal-employee/modal-employee.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgxPaginationModule } from 'ngx-pagination';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeComponent } from './employee.component';



@NgModule({
  declarations: [
    EmployeeComponent,
    ModalEmployeeComponent
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
export class EmployeeModule { }
