import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DoctorPageComponent } from './doctor/doctor-page/doctor-page.component';
import { PatientAppointmentsPageComponent } from './patient/patient-appointments-page/patient-appointments-page.component';
import { PatientDashboardPageComponent } from './patient/patient-dashboard-page/patient-dashboard-page.component';
import { PatientPageComponent } from './patient/patient-page/patient-page.component';
import { PatientProfilePageComponent } from './patient/patient-profile-page/patient-profile-page.component';

const routes: Routes = [
  { path: 'patient/dashboard', component: PatientDashboardPageComponent },
  { path: 'patient/profile', component: PatientProfilePageComponent },
  { path: 'patient/appointments', component: PatientAppointmentsPageComponent },
  { path: '', redirectTo: '/patient/dashboard', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
