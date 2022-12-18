import { Injectable } from '@angular/core';
import { LoginModel } from 'app/shared/models/login-model';
import { User } from 'app/shared/models/user.model';
import jwtDecode from 'jwt-decode';
import { TokenStorageService } from '../auth/token-storage.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private tokenService: TokenStorageService) { }

  getUser(): User {
    const token = this.tokenService.getToken() || '';
    const data = jwtDecode<LoginModel>(token);
    return {
      id: data.userId,
      email: data.email,
      role: data.role,
    } as User;
  }
}
