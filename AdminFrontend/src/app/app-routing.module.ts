import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Views/login/login.component';
import { PetServiceComponent } from './Views/pet-service/pet-service.component';
import { RoleGuardService as RoleGuard } from './auth/role-guard-service.service';
import { DashboardComponent } from './Views/dashboard/dashboard.component';
import { PetServiceOptionsComponent } from './Views/pet-service-options/pet-service-options.component';
import { EmployeeComponent } from './Views/employee/employee.component';
import { CustomerComponent } from './Views/customer/customer.component';
import { ServiceSellBillComponent } from './Views/service-sell-bill/service-sell-bill.component';
import { BlogCategory } from './Class/BlogCategory';
import { BlogCategoryComponent } from './Views/blog-category/blog-category.component';
import { BlogComponent } from './Views/blog/blog.component';
import { ShiftComponent } from './Views/shift/shift.component';
import { CaringShiftComponent } from './Views/caring-shift/caring-shift.component';
const routes: Routes = [
  {path:"petservice", component:PetServiceComponent,
  canActivate:[RoleGuard],
  data:{expectedRole:['admin','managementStaff']}},

  {path:"serviceoption", component:PetServiceOptionsComponent,
  canActivate:[RoleGuard],
  data:{expectedRole:['admin','managementStaff']}},
  
  {path:"employee", component:EmployeeComponent,
  canActivate:[RoleGuard],
  data:{expectedRole:['admin','managementStaff']}},

  {path:"customer", component:CustomerComponent,
  canActivate:[RoleGuard],
  data:{expectedRole:['admin','managementStaff']}},

  {path:"sellbill", component:ServiceSellBillComponent,
  canActivate:[RoleGuard],
  data:{expectedRole:['admin','managementStaff']}},

  {path:"blogcategory", component:BlogCategoryComponent,
  canActivate:[RoleGuard],
  data:{expectedRole:['admin','managementStaff']}},

  {path:"blog", component:BlogComponent,
  canActivate:[RoleGuard],
  data:{expectedRole:['admin','managementStaff']}},
  {path:"shift", component:ShiftComponent,
  canActivate:[RoleGuard],
  data:{expectedRole:['admin','managementStaff']}},
  
  {path:"caringShift", component:CaringShiftComponent,
  canActivate:[RoleGuard],
  data:{expectedRole:['caringStaff','admin','managementStaff']}},

  {path:"dashboard", component:DashboardComponent},

  {path:"login", component:LoginComponent},
  {path:"",  pathMatch: 'full',redirectTo: 'login'},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
