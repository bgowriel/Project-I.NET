import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable } from 'rxjs';
import { Appointment } from '../shared/models/appointment.model';

const baseUrl = 'https://localhost:7221/';
const apiUrl: string = 'api/appointments';


@Injectable()
export class PatientService {
  constructor(private http: HttpClient) { }

  public createAppointment(appointment: Appointment): Observable<any> {
    return this.http.post(baseUrl + apiUrl, appointment);
  }

}
