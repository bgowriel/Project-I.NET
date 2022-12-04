import { Component } from '@angular/core';
import { UserService } from 'app/services/user.service';
import { User } from '../models/user.model';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css']
})
export class SidenavComponent {
  public selected: boolean = false;

  public user: User;
  constructor(private userService: UserService) { }

  ngOnInit() {  
    this.user = this.userService.getUser();
    console.log(this.user);
  }
}
