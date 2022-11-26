import { Component, EventEmitter, Inject, OnChanges, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Appointment } from 'app/shared/models/appointment.model';

@Component({
  selector: 'app-add-appointment-modal',
  templateUrl: './add-appointment-modal.component.html',
  styleUrls: ['./add-appointment-modal.component.css']
})
export class AddAppointmentModalComponent implements OnInit {
  
  @Output() createAppointment: EventEmitter<any> = new EventEmitter();
  
  public form: FormGroup;
  public appointment: Appointment = new Appointment();

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }


  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.form = new FormGroup({
      doctors: new FormControl('', Validators.required),
      cabinets: new FormControl('', Validators.required),
      date: new FormControl('', Validators.required),
    })
  }

  sendRequest() {
    this.createAppointment.emit(this.appointment);
  }
}
