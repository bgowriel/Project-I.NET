import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppointmentService } from 'app/services/appointment/appointment.service';
import { Appointment } from 'app/shared/models/appointment.model';
import { ToasterService } from 'app/shared/toaster/toaster.service';
import { PatientService } from '../../services/patient/patient.service';

@Component({
  selector: 'app-patient-dashboard-page',
  templateUrl: './patient-dashboard-page.component.html',
  styleUrls: ['./patient-dashboard-page.component.css'],
  providers: [PatientService, ToasterService, AppointmentService]
})
export class PatientDashboardPageComponent implements OnInit {

  constructor(private patientService: PatientService, private toasterService: ToasterService, private route: ActivatedRoute, private appointmetService: AppointmentService) { }

  public data: any;

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.data = params['name'];
    });

    console.log(this.data)
  }

  async createNewAppointment(appointment: Appointment) {
    const result = await this.appointmetService.createAppointment(appointment)
      .toPromise().catch(error => error);

    if (!result.ok) {
      this.toasterService.onError("Something went wrong !");
    }
    else {
      this.toasterService.onSuccess("Appointment created successfully !");
    }
  }

}
