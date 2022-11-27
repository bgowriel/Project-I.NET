import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Appointment } from 'app/shared/models/appointment.model';
import { ToasterService } from 'app/shared/toaster/toaster.service';
import { PatientService } from '../patient.service';

@Component({
  selector: 'app-patient-page',
  templateUrl: './patient-page.component.html',
  styleUrls: ['./patient-page.component.css'],
  providers: [PatientService, ToasterService]
})
export class PatientPageComponent implements OnInit {
  constructor(private patientService: PatientService, private toasterService: ToasterService, private route: ActivatedRoute) {  }

  public data: any;

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.data = params['name'];
    });

    console.log(this.data)
  }

}
