import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbCarouselModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './Views/home/home.component';
import { SharedModule } from './Views/shared/shared.module';
import { AboutComponent } from './Views/about/about.component';
import { ProductListComponent } from './Views/product-list/product-list.component';
import { ProductSingleComponent } from './Views/product-single/product-single.component';
import { ServiceSingleComponent } from './Views/service-single/service-single.component';
import { BookingComponent } from './Views/booking/booking.component';
import { LoginComponent } from './Views/login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { NgxPaginationModule } from 'ngx-pagination';
import { CKEditor4, CKEditorModule } from 'ckeditor4-angular';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SignUpsComponent } from './Views/sign-ups/sign-ups.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AboutComponent,
    ProductListComponent,
    ProductSingleComponent,
    ServiceSingleComponent,
    BookingComponent,
    LoginComponent,
    SignUpsComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgxPaginationModule,
    SharedModule,
    NgbCarouselModule ,
    CKEditorModule,
    CommonModule,
    ReactiveFormsModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
