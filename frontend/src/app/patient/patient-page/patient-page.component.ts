import { Component } from '@angular/core';
import { Appointment } from 'app/shared/models/appointment.model';
import { ToasterService } from 'app/shared/toaster/toaster.service';
import { PatientService } from '../patient.service';

@Component({
  selector: 'app-patient-page',
  templateUrl: './patient-page.component.html',
  styleUrls: ['./patient-page.component.css'],
  providers: [PatientService, ToasterService]
})
export class PatientPageComponent {
  constructor(private patientService: PatientService, private toasterService: ToasterService) { }

  async createNewAppointment(appointment: Appointment) {
    const result = await this.patientService.createAppointment(appointment)
      .toPromise().catch(error => error);

    if (!result.ok) {
      this.toasterService.onError("Something went wrong !");
    }
    else {
      this.toasterService.onSuccess("Appointment created successfully !");
    }
  }
}
