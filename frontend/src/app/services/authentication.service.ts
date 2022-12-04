import { HttpClient } from '@angular/common/http';
import { Injectable, Output } from '@angular/core';
import { User } from 'app/shared/models/user.model';
import { Observable } from 'rxjs';

const AUTH_API = 'https://localhost:7221/api/users/';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  isAuthenticated = false;

  constructor(private http: HttpClient) { }

  login(email: string, password: string) {
    return this.http.post(AUTH_API + 'login', {email, password});
  }

  register(user: User) : Observable<User> {
    console.log(user);
    return this.http.post<User>(AUTH_API + 'register', user);
  }

  setAuthenticated(isAuthenticated: boolean) {
    this.isAuthenticated = isAuthenticated;
  }
}
