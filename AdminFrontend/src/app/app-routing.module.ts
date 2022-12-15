import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PetServiceComponent } from './Views/pet-service/pet-service.component';

const routes: Routes = [
  {path:"petService", component:PetServiceComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
