import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxPaginationModule } from 'ngx-pagination';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FooterComponent } from './Views/shared/footer/footer.component';
import { HeaderComponent } from './Views/shared/header/header.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PetServiceModule } from './Views/pet-service/pet-service.module';
import { ProfileEditorComponent } from './profile-editor/profile-editor.component';
import { LoginComponent } from './Views/login/login.component';
import { DashboardComponent } from './Views/dashboard/dashboard.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';


@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HeaderComponent,
    ProfileEditorComponent,
    LoginComponent,
    DashboardComponent
  ],
  imports: [
    PetServiceModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgxPaginationModule,
    FormsModule,
    BrowserAnimationsModule,
    NgbModule,
    CKEditorModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
