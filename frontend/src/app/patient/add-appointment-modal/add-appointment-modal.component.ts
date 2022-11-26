import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatCalendarCellClassFunction } from '@angular/material/datepicker';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Appointment } from 'app/shared/models/appointment.model';

@Component({
  selector: 'app-add-appointment-modal',
  templateUrl: './add-appointment-modal.component.html',
  styleUrls: ['./add-appointment-modal.component.css'],
})
export class AddAppointmentModalComponent implements OnInit {
  public form: FormGroup;
  public appointment: Appointment = new Appointment();

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.initForm();
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
  ]

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
