import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Doctor } from 'app/shared/models/doctor.model';
import { Office } from 'app/shared/models/office.model';
import { catchError, map, Observable } from 'rxjs';
import { Appointment } from '../shared/models/appointment.model';

const baseUrl = 'https://localhost:7221/';
const appointmentsApi: string = 'api/appointments';
const officesApi: string = 'api/offices'

@Injectable()
export class PatientService {
  constructor(private http: HttpClient) { }

  public createAppointment(appointment: Appointment): Observable<any> {
    return this.http.post(baseUrl + appointmentsApi, appointment);
  }
  public getAppointments(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(baseUrl + appointmentsApi);
  }
  public getAppointmentsByPatientId(id: string): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(baseUrl + appointmentsApi + '/patient/' + id);
  }
  public getAllOffices(): Observable<Office[]> {
    return this.http.get<Office[]>(baseUrl + officesApi);
  }
  public getDoctorsByOfficeId(id: string): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(baseUrl + 'doctors/' + id);
  }
  public getDoctorById(id: string): Observable<Doctor> {
    return this.http.get<Doctor>(baseUrl + 'api/users/get-user/' + id);
  }
  public getOfficeById(id: string): Observable<Doctor> {
    return this.http.get<Doctor>(baseUrl + 'api/offices/' + id);
  }
  public updateAppointment(id: string, appointment: Appointment): Observable<any> {
    return this.http.put<any>(baseUrl + 'api/appointments/' + id, appointment);
  }
  public deleteAppointment(id: string): Observable<any> {
    return this.http.delete<any>(baseUrl + 'api/appointments/' + id);
  }

}
