import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Views/login/login.component';
import { PetServiceComponent } from './Views/pet-service/pet-service.component';
import { RoleGuardService as RoleGuard } from './auth/role-guard-service.service';
import { DashboardComponent } from './Views/dashboard/dashboard.component';
const routes: Routes = [
  {path:"petservice", component:PetServiceComponent,
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
