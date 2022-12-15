import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ServiceOptionsComponent } from './service-options/service-options.component';
import { HttpClientModule } from '@angular/common/http';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CreateModalPetServiceComponent } from './create-modal-pet-service/create-modal-pet-service.component';
import { PetServiceComponent } from './pet-service.component';



@NgModule({
  declarations: [
    PetServiceComponent,
    ServiceOptionsComponent,
    CreateModalPetServiceComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    NgxPaginationModule,
    FormsModule,
    BrowserAnimationsModule,
    NgbModule,
    ReactiveFormsModule
  ]
})
export class PetServiceModule { }
