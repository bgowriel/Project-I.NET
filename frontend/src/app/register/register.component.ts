import { Component } from '@angular/core';
import { User } from '../shared/models/user.model';
import { PatientService } from '../services/patient/patient.service';
import { Router } from '@angular/router';
import { AuthenticationService } from 'app/services/auth/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers: [PatientService]
})
export class RegisterComponent {
  submitted = false;
  roles = ['Doctor', 'Patient'];

  user = new User();

  constructor(private authService: AuthenticationService,
    private router: Router) { }

  public register() {
    this.authService.register(this.user).subscribe(
      (data: User) => {
        console.log(data);
        this.router.navigate(['/login']);
      },
      (error: any) => {
        console.log(error);
      }
    );
  }

  public onSubmit() {
    console.log(this.user);
    this.submitted = true;
    this.register();
  }
}
