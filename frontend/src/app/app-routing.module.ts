import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PatientAppointmentsPageComponent } from './patient/patient-appointments-page/patient-appointments-page.component';
import { PatientDashboardPageComponent } from './patient/patient-dashboard-page/patient-dashboard-page.component';
import { PatientProfilePageComponent } from './patient/patient-profile-page/patient-profile-page.component';
import { HomeComponent } from './shared/home/home.component';
import { PageNotFoundComponent } from './shared/page-not-found/page-not-found.component';

const routes: Routes = [
  { path: 'patient/dashboard', component: PatientDashboardPageComponent },
  { path: 'patient/profile', component: PatientProfilePageComponent },
  { path: 'patient/appointments', component: PatientAppointmentsPageComponent },
  { path: '404', component: PageNotFoundComponent },
  { path: 'home', component: HomeComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', redirectTo: '404', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
