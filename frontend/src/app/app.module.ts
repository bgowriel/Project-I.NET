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

@NgModule({
  declarations: [
    AppComponent,
    SidenavComponent,
    PatientPageComponent,
    DoctorPageComponent,
    DashboardComponent,
    WorkTableComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    MatButtonModule,
    MatListModule,
    MatIconModule,
    MatTableModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
