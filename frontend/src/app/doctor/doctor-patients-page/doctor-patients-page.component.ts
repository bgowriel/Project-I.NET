import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { PatientService } from 'app/services/patient/patient.service';
import { UserService } from 'app/services/user/user.service';
import { User } from 'app/shared/models/user.model';
import { ToasterService } from 'app/shared/toaster/toaster.service';
import { DoctorService } from '../../services/doctor/doctor.service';
import { Appointment } from 'app/shared/models/appointment.model';
import { Doctor } from 'app/shared/models/doctor.model';
import { Office } from 'app/shared/models/office.model';
import { AppointmentService } from 'app/services/appointment/appointment.service';
import { OfficeService } from 'app/services/office/office.service';

@Component({
  selector: 'app-doctor-patients-page',
  templateUrl: './doctor-patients-page.component.html',
  styleUrls: ['./doctor-patients-page.component.css'],
  providers: [PatientService, ToasterService, DoctorService, OfficeService, AppointmentService]
})
export class DoctorPatientsPageComponent implements OnInit{
  constructor(public dialog: MatDialog, private patientService: PatientService , private doctorService: DoctorService, private toasterService: ToasterService, private userService: UserService, private appointmentService: AppointmentService, private officeService: OfficeService) { }

  public appointments: Appointment[] = [];
  public offices: Office[] = [];
  public doctors: Doctor[] = [];

  private dialogRef: any;

  displayedColumns: string[] = ['firstName','lastName', 'email','phoneNumber'];
  dataSourceCopy: any = [];
  dataSource: any = [];
  isLoading: boolean = false;

  @ViewChild(MatTable) table: MatTable<any>;

  public user: User;

  async ngOnInit() {
    this.isLoading = true;

    this.user = this.userService.getUser();
    // console.log(this.user);

    await this.getAppointmentsByDoctorId(this.user.id)

    this.isLoading = false;
  }

  async getAppointmentsByDoctorId(userId: string): Promise<void> {
    const result = await this.appointmentService.getAppointmentsByDoctorId(userId)
      .toPromise().catch(error => error);

    if (result) {

      try {
        this.dataSourceCopy = await this.mapAppointmentsData(result);
        this.dataSourceCopy.forEach(async (appointment: any)=>{
          if(appointment.status=="Approved"){
            this.dataSource.push(appointment);
          }
        })
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
      const patient = await this.patientService.getPatientById(appointment.patientId).toPromise().catch(error => error);
      appointment.patient = patient;
      console.log(patient)
    })

    return appointments;
  }
}
