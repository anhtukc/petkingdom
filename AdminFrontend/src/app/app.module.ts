import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxPaginationModule } from 'ngx-pagination';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PetServiceComponent } from './Views/pet-service/pet-service.component';
import { FooterComponent } from './Views/shared/footer/footer.component';
import { HeaderComponent } from './Views/shared/header/header.component';
import { ProductComponent } from './Views/product/product.component';
import { ServiceOptionsComponent } from './Views/pet-service/service-options/service-options.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CreateModalPetServiceComponent } from './Views/pet-service/create-modal-pet-service/create-modal-pet-service.component';
import { PetServiceModule } from './Views/pet-service/pet-service.module';
import { ProfileEditorComponent } from './profile-editor/profile-editor.component';

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HeaderComponent,
    ProfileEditorComponent
  ],
  imports: [
    PetServiceModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgxPaginationModule,
    FormsModule,
    BrowserAnimationsModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
