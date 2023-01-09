import { Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { AddAppointmentModalComponent } from 'app/patient/add-appointment-modal/add-appointment-modal.component';
import { AppointmentService } from 'app/services/appointment/appointment.service';
import { OfficeService } from 'app/services/office/office.service';
import { PatientService } from 'app/services/patient/patient.service';
import { UserService } from 'app/services/user/user.service';
import { Appointment } from 'app/shared/models/appointment.model';
import { Doctor } from 'app/shared/models/doctor.model';
import { Office } from 'app/shared/models/office.model';
import { User } from 'app/shared/models/user.model';
import { ToasterService } from 'app/shared/toaster/toaster.service';
import { Observable } from 'rxjs';
import { DoctorService } from '../../services/doctor/doctor.service';
import { BillService } from '../../services/bill/bill.service';

@Component({
  selector: 'app-doctor-appointments-page',
  templateUrl: './doctor-appointments-page.component.html',
  styleUrls: ['./doctor-appointments-page.component.css'],
  providers: [
    PatientService,
    ToasterService,
    DoctorService,
    OfficeService,
    AppointmentService,
  ],
})
export class DoctorAppointmentsPageComponent {
  constructor(
    public dialog: MatDialog,
    private patientService: PatientService,
    private doctorService: DoctorService,
    private toasterService: ToasterService,
    private userService: UserService,
    private appointmentService: AppointmentService,
    private officeService: OfficeService,
    private bill: BillService
  ) {}

  public appointments: Appointment[] = [];
  public offices: Office[] = [];
  public doctors: Doctor[] = [];

  private dialogRef: any;

  displayedColumns: string[] = [
    'patient',
    'email',
    'date',
    'hour',
    'description',
    'status',
    'actions',
  ];
  dataSource: any = [];
  isLoading: boolean = false;

  @ViewChild(MatTable) table: MatTable<any>;

  public user: User;

  async ngOnInit() {
    this.isLoading = true;

    this.user = this.userService.getUser();
    console.log(this.user);

    await this.getAppointmentsByDoctorId(this.user.id);

    this.isLoading = false;
  }

  async getAppointmentsByDoctorId(userId: string): Promise<void> {
    const result = await this.appointmentService
      .getAppointmentsByDoctorId(userId)
      .toPromise()
      .catch((error) => error);

    if (result) {
      try {
        this.dataSource = await this.mapAppointmentsData(result);
      } catch (error) {
        this.toasterService.onError('Something went wrong !');
        this.isLoading = false;
      }
    } else if (!result) {
      this.toasterService.onError('Something went wrong !');
    }
  }

  async mapAppointmentsData(appointments: any): Promise<void> {
    appointments.forEach(async (appointment: any) => {
      const patient = await this.patientService
        .getPatientById(appointment.patientId)
        .toPromise()
        .catch((error) => error);
      appointment.patient = patient;
      console.log(patient);
    });

    return appointments;
  }

  async onApproveBtnClick(id: string, appointment: Appointment) {
    appointment.status = 'Approved';
    const result = await this.appointmentService
      .updateAppointment(id, appointment)
      .toPromise()
      .catch((error) => error);

    if (result) {
      this.toasterService.onSuccess('Appointment approved successfully !');
      this.getAppointmentsByDoctorId(this.user.id);
    } else if (!result.ok) {
      this.toasterService.onError('Something went wrong !');
    }
  }

  async onCancelBtnClick(id: string, appointment: Appointment) {
    appointment.status = 'Canceled';
    const result = await this.appointmentService
      .updateAppointment(id, appointment)
      .toPromise()
      .catch((error) => error);

    if (result) {
      this.toasterService.onSuccess('Appointment canceled successfully !');
      this.getAppointmentsByDoctorId(this.user.id);
    } else if (!result.ok) {
      this.toasterService.onError('Something went wrong !');
    }
  }
}
