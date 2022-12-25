import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FooterComponent } from './Views/shared/footer/footer.component';
import { HeaderComponent } from './Views/shared/header/header.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PetServiceModule } from './Views/pet-service/pet-service.module';
import { ProfileEditorComponent } from './profile-editor/profile-editor.component';
import { LoginComponent } from './Views/login/login.component';
import { DashboardComponent } from './Views/dashboard/dashboard.component';
import { SanitizerUrlPipe } from './Helper/sanitizer-url.pipe';
import { PetServiceOptionsComponent } from './Views/pet-service-options/pet-service-options.component';
import { FormsModule } from '@angular/forms';
import {CKEditorModule} from '@ckeditor/ckeditor5-angular';
import { NgxPaginationModule } from 'ngx-pagination';
import { ModalServiceOptionComponent } from './Views/pet-service-options/modal-service-option/modal-service-option.component';
import { ServiceOptionModule } from './Views/pet-service-options/service-option-module.module';

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HeaderComponent,
    ProfileEditorComponent,
    LoginComponent,
    DashboardComponent,
    
  ],
  imports: [
    PetServiceModule,
    ServiceOptionModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    NgbModule,
    CKEditorModule,
    NgxPaginationModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
