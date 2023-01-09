import {
  Component,
  EventEmitter,
  HostListener,
  Inject,
  OnInit,
  Output,
} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserService } from 'app/services/user/user.service';
import { BillService } from 'app/services/bill/bill.service';
import { Appointment } from 'app/shared/models/appointment.model';
import { User } from 'app/shared/models/user.model';
import { Bill } from 'app/shared/models/bill.model';
import { DoctorService } from 'app/services/doctor/doctor.service';
import { AppointmentService } from 'app/services/appointment/appointment.service';
@Component({
  selector: 'app-add-appointment-modal',
  templateUrl: './add-appointment-modal.component.html',
  styleUrls: ['./add-appointment-modal.component.css'],
})
export class AddAppointmentModalComponent implements OnInit {
  public form: FormGroup;
  public appointment: Appointment = new Appointment();
  public bill: Bill = new Bill();

  @Output() onOfficeSelected: EventEmitter<any> = new EventEmitter();
  @Output() onDateSelected: EventEmitter<any> = new EventEmitter();

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private userService: UserService,
    private billService: BillService,
    private appointmentService: AppointmentService
  ) {}

  user: User;

  ngOnInit() {
    this.initForm();

    this.user = this.userService.getUser();
    this.appointment.patientId = this.user.id.toString();
    this.appointment.status = 'Pending';
  }

  public availableDates = [
    {
      date: new Date('27-Nov-2022'),
      free: true,
    },
    {
      date: new Date('29-Nov-2022'),
      free: false,
    },
    {
      date: new Date('30-Nov-2022'),
      free: false,
    },
    {
      date: new Date('04-Dec-2022'),
      free: false,
    },
    {
      date: new Date('06-Dec-2022'),
      free: false,
    },
    {
      date: new Date('11-Dec-2022'),
      free: false,
    },
    {
      date: new Date('12-Dec-2022'),
      free: false,
    },
    {
      date: new Date('14-Dec-2022'),
      free: false,
    },
    {
      date: new Date('18-Dec-2022'),
      free: false,
    },
  ];

  public hours = [8, 9, 10, 11, 12, 13, 14, 15, 16];

  officeSelected(id: string) {
    if (id) {
      this.onOfficeSelected.emit(id);
    }
  }

  dateSelected(date: Date) {
    if (date) {
      let nextDate: Date = new Date(date);
      nextDate.setDate(nextDate.getDate() + 1);

      this.appointmentService
        .getAppointmentsByDoctorIdAndDate(this.appointment.doctorId, nextDate)
        .subscribe((res) => {
          res.forEach((appointment) => {
            if (
              appointment.status === 'Approved' &&
              this.hours.indexOf(appointment.hour) >= 0
            ) {
              this.hours.splice(this.hours.indexOf(appointment.hour), 1);
            }
          });
        });
    }
  }

  async onSendRequestClick() {
    let nextDate: Date = this.appointment.date;
    nextDate.setDate(nextDate.getDate() + 1);
    this.appointment.date = nextDate;
    this.bill.amount = 100;
    this.bill.date = new Date();
    this.bill.patientId = this.user.id.toString();
    this.bill.doctorId = this.appointment.doctorId;
    this.bill.description = this.appointment.description;
    const result = await this.billService
      .createBill(this.bill)
      .toPromise()
      .catch((error) => error);
    if (result instanceof Error) {
      console.log(result);
    }
  }

  initForm() {
    this.form = new FormGroup({
      doctors: new FormControl('', Validators.required),
      cabinets: new FormControl('', Validators.required),
      date: new FormControl('', Validators.required),
      hour: new FormControl('', Validators.required),
    });
  }

  myFilter = (d: Date | null): boolean => {
    var res = true;
    this.availableDates.forEach((avDay) => {
      if (d !== null) {
        if (avDay.date.toISOString() === d.toISOString()) {
          if (!avDay.free) res = false;
        }
        if (d.getDay() === 0 || d.getDay() === 6) res = false;
      }
    });
    return res;
  };
}
