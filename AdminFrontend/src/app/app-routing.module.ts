import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Views/login/login.component';
import { PetServiceComponent } from './Views/pet-service/pet-service.component';
import { RoleGuardService as RoleGuard } from './auth/role-guard-service.service';
import { DashboardComponent } from './Views/dashboard/dashboard.component';
import { PetServiceOptionsComponent } from './Views/pet-service-options/pet-service-options.component';
import { EmployeeComponent } from './Views/employee/employee.component';
import { CustomerComponent } from './Views/customer/customer.component';
const routes: Routes = [
  {path:"petservice", component:PetServiceComponent,
  canActivate:[RoleGuard],
  data:{expectedRole:'admin'||'managementStaff'}},

  {path:"serviceoption", component:PetServiceOptionsComponent,
  canActivate:[RoleGuard],
  data:{expectedRole:'admin'||'managementStaff'}},
  
  {path:"employee", component:EmployeeComponent,
  canActivate:[RoleGuard],
  data:{expectedRole:'admin'||'managementStaff'}},

  {path:"customer", component:CustomerComponent,
  canActivate:[RoleGuard],
  data:{expectedRole:'admin'||'managementStaff'}},

  {path:"dashboard", component:DashboardComponent,
  canActivate:[RoleGuard],
  data:{expectedRole:'admin'||'managementStaff'||'caringStaff'}},

  {path:"login", component:LoginComponent},
  {path:"",  pathMatch: 'full',redirectTo: 'login'},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
