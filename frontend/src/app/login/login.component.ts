import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'app/services/authentication.service';
import { TokenStorageService } from 'app/services/token-storage.service';
import { LoginModel } from 'app/shared/models/login-model';
import jwtDecode from 'jwt-decode';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  isLoggedIn = false;
  isLoginFailed = false;

  errorMessage = '';
  roles: string[] = [];

  email = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl('', [Validators.required, Validators.minLength(8)]);

  loginForm = new FormGroup({
    email: this.email,
    password: this.password
  })

  hide = true;

  user!: LoginModel;

  constructor(private authService: AuthenticationService,
              private tokenService: TokenStorageService,
              private router: Router) { }

  ngOnInit() {
    if (this.authService.isAuthenticated) {
      this.router.navigate(['/', 'dashboard']);
    }

    if (this.tokenService.getToken()) {
      this.isLoggedIn = true;
      this.roles = this.tokenService.getUser().roles;
    }
  }

  onLogin() {
    if (this.loginForm.valid) {
      this.authService.login(
        this.email.value ? this.email.value : '',
        this.password.value ? this.password.value : ''
      )
        .subscribe(
          (data: any) => {
          console.log(data);
          console.log(jwtDecode(data.token));

          this.user = jwtDecode(data.token);

          this.tokenService.setEmail(this.user.email);
          
          console.log(this.user);
          this.authService.setAuthenticated(true);

          if (this.user.role[1] == 'Admin') {
            this.tokenService.setAdminRole(true);
          } else {
            this.tokenService.setAdminRole(false);
          }
          
          this.tokenService.saveToken(data.token);
          this.tokenService.saveUser(data);

          this.isLoggedIn = true;
          this.roles = this.tokenService.getUser().roles;
          this.router.navigate(['/', 'patient', 'dashboard']);
        },
        (err) => {
          console.log(err);
          if (err.error.message) {
            this.errorMessage = err.error.message;
          } else {
            this.errorMessage = "Login failed. Please check your credentials.";
          }
          this.isLoginFailed = true;
        }
      );
    }
  }

  reloadPage() {
    window.location.reload();
  }

  goToRegister() {
    this.router.navigate(['/', 'register']);
  }

  getErrorMessage() {
    if (this.email.hasError('required')) {
      return 'You must enter a value';
    }

    return this.email.hasError('email') ? 'Not a valid email' : '';
  }
}