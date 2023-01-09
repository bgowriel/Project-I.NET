import { Component } from '@angular/core';
import { AuthenticationService } from 'app/services/auth/authentication.service';
import { UserService } from 'app/services/user/user.service';
import { User } from '../models/user.model';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css']
})
export class SidenavComponent {
  public selected: boolean = false;

  public user: User;
  constructor(private userService: UserService,
  private authService: AuthenticationService) { }

  ngOnInit() {  
    this.user = this.userService.getUser();
    console.log(this.user);
  }

  public logout() {
    this.userService.logout();
    this.authService.setAuthenticated(false);
  }
}
