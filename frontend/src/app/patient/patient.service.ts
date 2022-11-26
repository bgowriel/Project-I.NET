import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Appointment } from '../shared/models/appointment.model';

const apiUrl: string = 'api/appointments';

@Injectable()
export class PatientService {
  constructor(private http: HttpClient) {}

  createAppointment(appointment: Appointment) {
    console.log('post request')
    this.http
      .post('https://localhost:7221/' + apiUrl, appointment)
      .subscribe((response) => console.log(response));
  }
}
