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
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { CKEditor4, CKEditorModule } from 'ckeditor4-angular';
import { FilterByDatePipe } from './pipe/FilterByDatePipe ';
import { DatePipe } from '@angular/common';

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
    FilterByDatePipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    NgxPaginationModule,
    SharedModule,
    NgbCarouselModule ,
    CKEditorModule,
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
