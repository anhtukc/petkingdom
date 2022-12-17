import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './Views/about/about.component';
import { BookingComponent } from './Views/booking/booking.component';
import { HomeComponent } from './Views/home/home.component';
import { ProductListComponent } from './Views/product-list/product-list.component';
import { ProductSingleComponent } from './Views/product-single/product-single.component';
import { ServiceSingleComponent } from './Views/service-single/service-single.component';

const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: "about", component: AboutComponent },
  { path: "productlist", component: ProductListComponent, children: [] },
  {path: "product-single", component: ProductSingleComponent},
  {path: "service-single", component: ServiceSingleComponent},
  {path: "booking", component: BookingComponent},

  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
