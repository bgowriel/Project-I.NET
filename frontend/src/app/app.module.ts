import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SidenavComponent } from './shared/sidenav/sidenav.component';
import { PatientPageComponent } from './patient/patient-page/patient-page.component';
import { DoctorPageComponent } from './doctor/doctor-page/doctor-page.component';
import { DashboardComponent } from './shared/dashboard/dashboard.component';
import { WorkTableComponent } from './shared/work-table/work-table.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field'
import { MatSelectModule } from '@angular/material/select'
import { MatInputModule } from '@angular/material/input'
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { HttpClientModule } from '@angular/common/http';

import { AddAppointmentModalComponent } from './patient/add-appointment-modal/add-appointment-modal.component';
import { MatNativeDateModule } from '@angular/material/core';
import { ToasterComponent } from './shared/toaster/toaster.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { CabinetComponent } from './shared/cabinet/cabinet.component';
import { PatientProfilePageComponent } from './patient/patient-profile-page/patient-profile-page.component';
import { PatientAppointmentsPageComponent } from './patient/patient-appointments-page/patient-appointments-page.component';
import { PatientDashboardPageComponent } from './patient/patient-dashboard-page/patient-dashboard-page.component';
import { MatCardModule } from '@angular/material/card';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './shared/home/home.component';
import { MatMenuModule } from '@angular/material/menu';
import { PageNotFoundComponent } from './shared/page-not-found/page-not-found.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { authInterceptorProviders } from './services/auth-interceptor.service';

@NgModule({
  declarations: [
    AppComponent,
    SidenavComponent,
    PatientPageComponent,
    DoctorPageComponent,
    DashboardComponent,
    WorkTableComponent,
    AddAppointmentModalComponent,
    ToasterComponent,
    CabinetComponent,
    PatientProfilePageComponent,
    PatientAppointmentsPageComponent,
    PatientDashboardPageComponent,
    RegisterComponent,
    LoginComponent,

    HomeComponent,
    PageNotFoundComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    MatButtonModule,
    MatListModule,
    MatIconModule,
    MatTableModule,
    MatFormFieldModule,
    FormsModule,
    MatSelectModule,
    MatInputModule,
    MatDatepickerModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    HttpClientModule,
    MatSnackBarModule,
    MatCardModule,
    MatMenuModule,
    MatTooltipModule,
    MatProgressSpinnerModule
  ],
    providers: [authInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
