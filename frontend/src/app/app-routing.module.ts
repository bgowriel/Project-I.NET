import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DoctorPageComponent } from './doctor/doctor-page/doctor-page.component';
import { PatientPageComponent } from './patient/patient-page/patient-page.component';

const routes: Routes = [
  { path: 'patient-dashboard', component: PatientPageComponent },
  { path: 'doctor-dashboard', component: DoctorPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
