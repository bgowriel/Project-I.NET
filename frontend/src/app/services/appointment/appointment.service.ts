import {
  HttpClient,
  HttpErrorResponse,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Doctor } from 'app/shared/models/doctor.model';
import { Office } from 'app/shared/models/office.model';
import { User } from 'app/shared/models/user.model';
import { catchError, map, Observable } from 'rxjs';
import { Appointment } from '../../shared/models/appointment.model';

const baseUrl = 'https://localhost:7221/';
const appointmentsApi: string = 'api/appointments';

@Injectable({
  providedIn: 'root',
})
export class AppointmentService {
  constructor(private http: HttpClient) {}

  public getAppointments(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(baseUrl + appointmentsApi);
  }

  public getAppointmentsByPatientId(id: string): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(
      baseUrl + appointmentsApi + '/patient/' + id
    );
  }

  public getAppointmentsByDoctorId(id: string): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(
      baseUrl + appointmentsApi + '/doctor/' + id
    );
  }

  public createAppointment(appointment: Appointment): Observable<any> {
    return this.http.post(baseUrl + appointmentsApi, appointment);
  }

  public updateAppointment(
    id: string,
    appointment: Appointment
  ): Observable<any> {
    return this.http.put<any>(baseUrl + 'api/appointments/' + id, appointment);
  }

  public deleteAppointment(id: string): Observable<any> {
    return this.http.delete<any>(baseUrl + 'api/appointments/' + id);
  }

  public getAppointmentsByDoctorIdAndDate(
    id: string,
    date: Date
  ): Observable<Appointment[]> {
    let jsonDate = date.toJSON();
    return this.http.get<Appointment[]>(
      baseUrl + appointmentsApi + '/doctor/' + id + '/date/' + jsonDate
    );
  }
}
