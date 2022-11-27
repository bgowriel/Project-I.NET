import { Component, EventEmitter, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Appointment } from 'app/shared/models/appointment.model';
import { Doctor } from 'app/shared/models/doctor.model';
import { Office } from 'app/shared/models/office.model';
import { ToasterService } from 'app/shared/toaster/toaster.service';
import { AddAppointmentModalComponent } from '../add-appointment-modal/add-appointment-modal.component';
import { PatientService } from '../patient.service';

@Component({
  selector: 'app-patient-appointments-page',
  templateUrl: './patient-appointments-page.component.html',
  styleUrls: ['./patient-appointments-page.component.css'],
  providers: [PatientService, ToasterService]
})
export class PatientAppointmentsPageComponent {
  constructor(public dialog: MatDialog, private patientService: PatientService, private toasterService: ToasterService) { }

  public appointments: Appointment[] = [];
  public offices: Office[] = [];
  public doctors: Doctor[] = [];

  private dialogRef: any;

  ngOnInit() {
    this.getAllOffices();
    this.getAllAppointments();
  }

  onCreateNewAppointmentClick() {
    this.dialogRef = this.dialog.open(AddAppointmentModalComponent, {
      data: {
        cabinets: this.offices,
        doctors: this.doctors
      },
      autoFocus: false
    });

    this.dialogRef.afterClosed().subscribe((result: Appointment) => {
      if (!result)
        return;
      this.createNewAppointment(result);
    })

    this.dialogRef.componentInstance.onOfficeSelected.subscribe((id: string) => {
      this.getDoctorsByOfficeId(id);
    })
  }

  async createNewAppointment(appointment: Appointment) {
    const result = await this.patientService.createAppointment(appointment)
      .toPromise().catch(error => error);

    if (result) {
      this.toasterService.onSuccess("Appointment created successfully !");
      this.appointments.unshift(appointment);
      console.log(appointment)
    }
    else if (!result.ok) {
      this.toasterService.onError("Something went wrong !");
    }
  }

  async getAllAppointments() {
    const result = await this.patientService.getAppointments()
      .toPromise().catch(error => error);

    if (result) {
      // this.toasterService.onSuccess("Appointments fetched successfully !");
      this.appointments = [...result];
      console.log(this.appointments)
    }
    else if (!result.ok) {
      this.toasterService.onError("Something went wrong !");
    }
  }

  async getAllOffices() {
    const result = await this.patientService.getAllOffices()
      .toPromise().catch(error => error);

    if (result) {
      // this.toasterService.onSuccess("Offices fetched successfully !");
      this.offices = [...result];
    }
    else if (!result.ok) {
      this.toasterService.onError("Something went wrong !");
    }

  }
  async getDoctorsByOfficeId(id: string) {
    const result = await this.patientService.getDoctorsByOfficeId(id)
      .toPromise().catch(error => error);

    console.log(result)
    if (result) {
      // this.toasterService.onSuccess("Doctors fetched successfully !");
      this.doctors = [...result];
      this.dialogRef.componentInstance.data.doctors = this.doctors;
    }
    else if (!result.ok) {
      this.toasterService.onError("Something went wrong !");
    }

  }
}
