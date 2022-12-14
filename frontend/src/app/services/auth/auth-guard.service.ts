import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private router: Router, private authService: AuthenticationService) { }

  canActivate(): boolean {
    if (this.authService.isLoggedIn()) {
      return true;
    }

    if (!this.authService.isAuthenticated) {
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }
}
