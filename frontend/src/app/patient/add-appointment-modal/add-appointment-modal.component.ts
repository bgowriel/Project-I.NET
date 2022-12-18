import { Component, EventEmitter, HostListener, Inject, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserService } from 'app/services/user/user.service';
import { Appointment } from 'app/shared/models/appointment.model';
import { User } from 'app/shared/models/user.model';

@Component({
  selector: 'app-add-appointment-modal',
  templateUrl: './add-appointment-modal.component.html',
  styleUrls: ['./add-appointment-modal.component.css'],
})

export class AddAppointmentModalComponent implements OnInit {
  public form: FormGroup;
  public appointment: Appointment = new Appointment();

  @Output() onOfficeSelected: EventEmitter<any> = new EventEmitter();

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private userService: UserService) { }

  user: User;
  
  ngOnInit() {
    this.initForm();

    this.user = this.userService.getUser()
    this.appointment.patientId = this.user.id.toString();
    this.appointment.status = "Pending"
  }

  public availableDates = [
    {
      date: new Date('27-Nov-2022'),
      free: true
    },
    {
      date: new Date('29-Nov-2022'),
      free: false
    },
    {
      date: new Date('30-Nov-2022'),
      free: false
    },
    {
      date: new Date('04-Dec-2022'),
      free: false
    },
    {
      date: new Date('06-Dec-2022'),
      free: false
    },
    {
      date: new Date('11-Dec-2022'),
      free: false
    },
    {
      date: new Date('12-Dec-2022'),
      free: false
    },
    {
      date: new Date('14-Dec-2022'),
      free: false
    },
    {
      date: new Date('18-Dec-2022'),
      free: false
    },
  ]

  officeSelected(id: string) {
    if (id) {
      this.onOfficeSelected.emit(id)
    }
  }

  initForm() {
    this.form = new FormGroup({
      doctors: new FormControl('', Validators.required),
      cabinets: new FormControl('', Validators.required),
      date: new FormControl('', Validators.required),
    })
  }

  myFilter = (d: Date | null): boolean => {
    var res = true;
    this.availableDates.forEach(avDay => {
      if (d !== null) {
        if (avDay.date.toISOString() === d.toISOString()) {
          if (!avDay.free)
            res = false;
        }
        if (d.getDay() === 0 || d.getDay() === 6)
          res = false
      }
    })
    return res;
  }
}
