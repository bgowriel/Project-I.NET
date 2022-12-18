import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminPageComponent } from './admin/admin-page/admin-page.component';
import { DoctorAppointmentsPageComponent } from './doctor/doctor-appointments-page/doctor-appointments-page.component';
import { DoctorDashboardPageComponent } from './doctor/doctor-dashboard-page/doctor-dashboard-page.component';
import { DoctorJoinOfficePageComponent } from './doctor/doctor-join-office-page/doctor-join-office-page.component';
import { DoctorPatientsPageComponent } from './doctor/doctor-patients-page/doctor-patients-page.component';
import { DoctorProfilePageComponent } from './doctor/doctor-profile-page/doctor-profile-page.component';
import { LoginComponent } from './login/login.component';
import { AddOfficePageComponent } from './patient/add-office-page/add-office-page.component';
import { PatientAppointmentsPageComponent } from './patient/patient-appointments-page/patient-appointments-page.component';
import { PatientDashboardPageComponent } from './patient/patient-dashboard-page/patient-dashboard-page.component';
import { PatientProfilePageComponent } from './patient/patient-profile-page/patient-profile-page.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuardService } from './services/auth/auth-guard.service';
import { HomeComponent } from './shared/home/home.component';
import { PageNotFoundComponent } from './shared/page-not-found/page-not-found.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'patient/dashboard', data: {role: 'Patient'}, canActivate: [AuthGuardService], component: PatientDashboardPageComponent },
  { path: 'patient/profile', data: {role: 'Patient'}, canActivate: [AuthGuardService], component: PatientProfilePageComponent },
  { path: 'patient/appointments', data: {role: 'Patient'}, canActivate: [AuthGuardService], component: PatientAppointmentsPageComponent },
  { path: 'patient/register-office', data: {role: 'Patient'}, canActivate: [AuthGuardService], component: AddOfficePageComponent },
  { path: 'doctor/dashboard', data: {role: 'Doctor'}, canActivate: [AuthGuardService], component: DoctorDashboardPageComponent },
  { path: 'doctor/profile', data: {role: 'Doctor'}, canActivate: [AuthGuardService], component: DoctorProfilePageComponent },
  { path: 'doctor/appointments', data: {role: 'Doctor'}, canActivate: [AuthGuardService], component: DoctorAppointmentsPageComponent },
  { path: 'doctor/patients', data: {role: 'Doctor'}, canActivate: [AuthGuardService], component: DoctorPatientsPageComponent },
  { path: 'doctor/join-office', data: {role: 'Doctor'}, canActivate: [AuthGuardService], component: DoctorJoinOfficePageComponent },
  { path: 'admin/dashboard', data: {role: 'Admin'}, canActivate: [AuthGuardService], component: AdminPageComponent},
  { path: 'home', component: HomeComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
