import { Component } from '@angular/core';
import { User } from '../shared/models/user.model';
import { PatientService } from '../patient/patient.service';
import { AuthenticationService } from 'app/services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers: [PatientService]
})
export class RegisterComponent {
  submitted = false;
  roles = [ 'Doctor', 'Patient'];

  user = new User();

  constructor(private authService: AuthenticationService,
              private router: Router) { }

  public register() {
    this.authService.register(this.user).subscribe(
      (data: User) => {
        console.log(data);
        this.router.navigate(['/patient/dashboard']);
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
