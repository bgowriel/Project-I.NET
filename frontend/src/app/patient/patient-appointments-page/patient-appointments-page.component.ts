import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { UserService } from 'app/services/user.service';
import { Appointment } from 'app/shared/models/appointment.model';
import { Doctor } from 'app/shared/models/doctor.model';
import { Office } from 'app/shared/models/office.model';
import { User } from 'app/shared/models/user.model';
import { ToasterService } from 'app/shared/toaster/toaster.service';
import { AddAppointmentModalComponent } from '../add-appointment-modal/add-appointment-modal.component';
import { PatientService } from '../patient.service';

@Component({
  selector: 'app-patient-appointments-page',
  templateUrl: './patient-appointments-page.component.html',
  styleUrls: ['./patient-appointments-page.component.css'],
  providers: [PatientService, ToasterService]
})
export class PatientAppointmentsPageComponent implements OnInit {
  constructor(public dialog: MatDialog, private patientService: PatientService, private toasterService: ToasterService, private userService: UserService) { }

  public appointments: Appointment[] = [];
  public offices: Office[] = [];
  public doctors: Doctor[] = [];

  private dialogRef: any;

  displayedColumns: string[] = ['office', 'doctor', 'date', 'description', 'status', 'actions'];
  dataSource: any = [];
  isLoading: boolean = false;

  @ViewChild(MatTable) table: MatTable<any>;

  public user: User;

  async ngOnInit() {
    this.isLoading = true;

    this.user = this.userService.getUser();


    await this.getAppointmentsByPatientId(this.user.id)
    await this.getAllOffices();

    this.isLoading = false;
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
      const doctor = await this.patientService.getDoctorById(appointment.doctorId).toPromise().catch(error => error);
      const office = await this.patientService.getOfficeById(appointment.officeId).toPromise().catch(error => error);
      appointment.doctor = doctor;
      appointment.office = office;

      this.dataSource.unshift(appointment);
      this.table.renderRows();
    }
    else if (!result.ok) {
      this.toasterService.onError("Something went wrong !");
    }
  }

  async getAppointmentsByPatientId(userId: string): Promise<void> {
    const result = await this.patientService.getAppointmentsByPatientId(userId)
      .toPromise().catch(error => error);

    if (result) {
      try {
        this.dataSource = await this.mapAppointmentsData(result);
      } catch (error) {
        this.toasterService.onError("Something went wrong !");
        this.isLoading = false
      }
    }
    else if (!result) {
      this.toasterService.onError("Something went wrong !");
    }
  }

  async mapAppointmentsData(appointments: any): Promise<void> {
    appointments.forEach(async (appointment: any) => {
      const doctor = await this.patientService.getDoctorById(appointment.doctorId).toPromise().catch(error => error);
      const office = await this.patientService.getOfficeById(appointment.officeId).toPromise().catch(error => error);
      appointment.doctor = doctor;
      appointment.office = office;
    })

    return appointments;
  }

  async getAllOffices(): Promise<void> {
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

  async onActionAppointmentClick(id: string, appointment: Appointment) {
    if (appointment.status === "Canceled") {
      await this.removeAppointment(id);
    } else {
      appointment.status = 'Canceled';
      const result = await this.patientService.updateAppointment(id, appointment)
        .toPromise().catch(error => error);

      if (result) {
        this.toasterService.onSuccess("Appointment canceled successfully !");
      }
      else if (!result.ok) {
        this.toasterService.onError("Something went wrong !");
      }
    }
  }

  async removeAppointment(id: string) {
    const result = await this.patientService.deleteAppointment(id)
      .toPromise().catch(error => error);

    if (result) {
      this.toasterService.onSuccess("Appointment removed successfully !");
      this.dataSource.splice(this.dataSource.findIndex((appointment: Appointment) => appointment.id === id), 1);
      this.table.renderRows();
    }
    else if (!result.ok) {
      this.toasterService.onError("Something went wrong !");
    }
  }

}
