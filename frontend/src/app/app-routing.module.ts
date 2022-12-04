import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DoctorPageComponent } from './doctor/doctor-page/doctor-page.component';
import { LoginComponent } from './login/login.component';
import { PatientAppointmentsPageComponent } from './patient/patient-appointments-page/patient-appointments-page.component';
import { PatientDashboardPageComponent } from './patient/patient-dashboard-page/patient-dashboard-page.component';
import { PatientProfilePageComponent } from './patient/patient-profile-page/patient-profile-page.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuardService } from './services/auth-guard.service';
import { HomeComponent } from './shared/home/home.component';
import { PageNotFoundComponent } from './shared/page-not-found/page-not-found.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent},
  { path: 'patient/dashboard', canActivate: [AuthGuardService], component: PatientDashboardPageComponent },
  { path: 'patient/profile', canActivate: [AuthGuardService], component: PatientProfilePageComponent },
  { path: 'patient/appointments', canActivate: [AuthGuardService], component: PatientAppointmentsPageComponent },
  { path: 'home', component: HomeComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: '404', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
