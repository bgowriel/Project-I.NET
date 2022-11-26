import { Component } from '@angular/core';
import { Appointment } from 'app/shared/models/appointment.model';
import { PatientService } from '../patient.service';

@Component({
  selector: 'app-patient-page',
  templateUrl: './patient-page.component.html',
  styleUrls: ['./patient-page.component.css'],
  providers: [PatientService]
})
export class PatientPageComponent {

  constructor(private patientService: PatientService) { }

  createNewAppointment(appointment: Appointment) {
    this.patientService.createAppointment(appointment);
  }
}
