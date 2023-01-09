import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { UserService } from 'app/services/user/user.service';
import { Appointment } from 'app/shared/models/appointment.model';
import { Doctor } from 'app/shared/models/doctor.model';
import { Office } from 'app/shared/models/office.model';
import { Bill } from 'app/shared/models/bill.model';
import { User } from 'app/shared/models/user.model';
import { ToasterService } from 'app/shared/toaster/toaster.service';
import { PatientService } from '../../services/patient/patient.service';
import { AppointmentService } from 'app/services/appointment/appointment.service';
import { DoctorService } from 'app/services/doctor/doctor.service';
import { OfficeService } from 'app/services/office/office.service';
import { BillService } from 'app/services/bill/bill.service';

@Component({
  selector: 'app-doctor-billing-page',
  templateUrl: './doctor-billing-page.component.html',
  styleUrls: ['./doctor-billing-page.component.css'],
  providers: [
    PatientService,
    ToasterService,
    DoctorService,
    AppointmentService,
    BillService,
  ],
})
export class DoctorBillingPageComponent implements OnInit {
  constructor(
    public dialog: MatDialog,
    private toasterService: ToasterService,
    private patientService: PatientService,
    private doctorService: DoctorService,
    private userService: UserService,
    private billService: BillService
  ) {}

  public bills: Bill[] = [];

  displayedColumns: string[] = [
    'doctor',
    'date',
    'description',
    'price',
  ];
  dataSource: any = [];
  isLoading: boolean = false;

  @ViewChild(MatTable) table: MatTable<any>;

  public user: User;

  async ngOnInit() {
    this.isLoading = true;

    this.user = this.userService.getUser();

    await this.getBillsById(this.user.id);

    this.isLoading = false;
  }

  async mapBillsData(bills: any): Promise<void> {
    bills.forEach(async (bill: any) => {
      const patient = await this.patientService
        .getPatientById(bill.patientId)
        .toPromise()
        .catch((error) => error);
      
        console.log(patient);

        bill.patient = patient;

    });

    return bills;
  }

  async getBillsById(userId: string): Promise<void> {
    const result = await this.billService
      .getBillsByDoctorId(userId)
      .toPromise()
      .catch((error) => error);

      console.log(result);

    if (result.length >= 0) {
      try {
        this.dataSource = await this.mapBillsData(result);
      } catch (error) {
        this.toasterService.onError('Something went wrong !');
        this.isLoading = false;
      }
    } else if (!result) {
      this.toasterService.onError('Something went wrong !');
    }
  }
}
