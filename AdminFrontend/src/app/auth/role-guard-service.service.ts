// src/app/auth/role-guard.service.ts
import { Injectable } from '@angular/core';
import { 
  Router,
  CanActivate,
  ActivatedRouteSnapshot
} from '@angular/router';
import { AuthService } from './auth-service.service';
import decode from 'jwt-decode';
@Injectable({
  providedIn: 'root'
})
export class RoleGuardService implements CanActivate {
  constructor(public auth: AuthService, public router: Router) {}
  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRole = route.data['expectedRole'];
    const token = localStorage.getItem('token');
    const tokenPayload:any = decode(token);
    if (
      !this.auth.isAuthenticated() || token == null
      
    ) {
      this.router.navigate(['login']);
      return false;
    }
    if(tokenPayload.role !== expectedRole){
      alert("Bạn không có quyền truy cập vào trang");
      this.router.navigate(['dashboard']);
    }
    return true;
  }
}
