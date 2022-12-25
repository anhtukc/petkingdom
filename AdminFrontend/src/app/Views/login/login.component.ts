import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { login } from 'src/app/Class/login';
import { LoginApiService } from './api-login.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private api: LoginApiService, public router: Router) { }
  public acc: login;
  errorAlert: string = "";
  ngOnInit(): void {
    this.acc = {
      username: "",
      password: ""
    };
  }

  login() {
    this.api.login(this.acc).subscribe(result => {
      if (result.message == 'fail') {
        if (result.details == 'Invalid account') {
          this.errorAlert = "Tài khoản hoặc mật khẩu không chính xác";
          alert(this.errorAlert);
        }
      }
      else if (result.message == 'success') {
        localStorage.setItem('token', result.token);
        this.router.navigate(['dashboard']);
      }
      else {
        console.log(result.details);
      }
    })
  }

  alert() {
    console.log(this.acc);
  }
}
