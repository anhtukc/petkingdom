import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CreateModalPetServiceComponent } from './create-modal-pet-service/create-modal-pet-service.component';
import { PetServiceComponent } from './pet-service.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { SanitizerUrlPipe } from 'src/app/Helper/sanitizer-url.pipe';
import { ModalServiceImageManagementComponent } from './modal-service-image-management/modal-service-image-management.component';
import { ModalUpdatePetServiceComponent } from './modal-update-pet-service/modal-update-pet-service.component';



@NgModule({
  declarations: [
    PetServiceComponent,
    CreateModalPetServiceComponent,
    SanitizerUrlPipe,
    ModalServiceImageManagementComponent,
    ModalUpdatePetServiceComponent
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
  ],
  exports:[SanitizerUrlPipe]
 
})
export class PetServiceModule { }
