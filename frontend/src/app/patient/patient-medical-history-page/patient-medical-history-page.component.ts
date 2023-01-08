import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { AppointmentService } from 'app/services/appointment/appointment.service';
import { DoctorService } from 'app/services/doctor/doctor.service';
import { OfficeService } from 'app/services/office/office.service';
import { PatientService } from 'app/services/patient/patient.service';
import { UserService } from 'app/services/user/user.service';
import { Appointment } from 'app/shared/models/appointment.model';
import { Doctor } from 'app/shared/models/doctor.model';
import { Office } from 'app/shared/models/office.model';
import { User } from 'app/shared/models/user.model';
import { ToasterService } from 'app/shared/toaster/toaster.service';
import { AddAppointmentModalComponent } from '../add-appointment-modal/add-appointment-modal.component';

@Component({
  selector: 'app-patient-medical-history-page',
  templateUrl: './patient-medical-history-page.component.html',
  styleUrls: ['./patient-medical-history-page.component.css'],
  providers: [PatientService, ToasterService, DoctorService, OfficeService, AppointmentService]
})
export class PatientMedicalHistoryPageComponent implements OnInit{
  constructor(public dialog: MatDialog, private officeService: OfficeService, private doctorService: DoctorService, private patientService: PatientService, private toasterService: ToasterService, private userService: UserService, private appointmentService: AppointmentService) { }

  public appointments: Appointment[] = [];
  public offices: Office[] = [];
  public doctors: Doctor[] = [];

  private dialogRef: any;

  displayedColumns: string[] = ['office', 'doctor', 'date', 'description'];
  dataSource: any = [];
  isLoading: boolean = false;

  @ViewChild(MatTable) table: MatTable<any>;

  public user: User;

  async ngOnInit() {
    this.isLoading = true;

    this.user = this.userService.getUser();

    await this.getAppointmentsByPatientId(this.user.id)

    this.isLoading = false;
  }

  async getAppointmentsByPatientId(userId: string): Promise<void> {
    const result = await this.appointmentService.getAppointmentsByPatientId(userId)
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

  mapAppointmentsData(appointments: any): Appointment[] {
    const validAppointments: Appointment[] = [];
    appointments.forEach(async (appointment: any) => {
      const doctor = await this.doctorService.getDoctorById(appointment.doctorId).toPromise().catch(error => error);
      const office = await this.officeService.getOfficeById(appointment.officeId).toPromise().catch(error => error);
      appointment.doctor = doctor;
      appointment.office = office;
    })
    appointments.forEach(async (appointment: any) => {
      if (appointment.status == "Approved"){
        validAppointments.push(appointment)
      }
    })

    return validAppointments;
  }

}
