import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './authentication.service';

const TOKEN_KEY = 'auth-token';
const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {
  private email: string = '';
  public isAdmin: boolean = false;

  constructor(private router: Router) {}

  signOut() {
    window.sessionStorage.clear();
    this.router.navigate(['/']);
  }

  public saveToken(token: string) {
    window.sessionStorage.removeItem(TOKEN_KEY);
    window.sessionStorage.setItem(TOKEN_KEY, token);
  }

  public getToken(): string | null {
    return window.sessionStorage.getItem(TOKEN_KEY);
  }

  public saveUser(user: any) {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY, JSON.stringify(user));
  }

  public getUser(): any {
    const user = window.sessionStorage.getItem(USER_KEY);
 
    if (user) {
      return JSON.parse(user);
    }

    return {};
  }

  public setAdminRole(isAdmin: boolean) {
    this.isAdmin = isAdmin;
  }

  public isLoggedIn(): boolean {
    return this.getToken() != null;
  }

  public getEmail(): string {
    return this.email;
  }

  public setEmail(email: string) {
    this.email = email;
  }
}
