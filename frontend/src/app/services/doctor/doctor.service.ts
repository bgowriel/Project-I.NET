import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Appointment } from 'app/shared/models/appointment.model';
import { Doctor } from 'app/shared/models/doctor.model';
import { Office } from 'app/shared/models/office.model';
import { User } from 'app/shared/models/user.model';
import { Observable } from 'rxjs';
import { AppointmentService } from '../appointment/appointment.service';

const baseUrl = 'https://localhost:7221/';
const appointmentsApi: string = 'api/appointments';
const officesApi: string = 'api/offices';
const updateUserApi: string = 'api/users/update-user';
const userApi: string = 'api/users/';

@Injectable({
  providedIn: 'root',
})
export class DoctorService {
  constructor(
    private http: HttpClient,
    private appointmentService: AppointmentService
  ) {}

  public getDoctorById(id: string): Observable<Doctor> {
    return this.http.get<Doctor>(baseUrl + 'api/users/' + id);
  }

  public getDoctorsByOfficeId(id: string): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(baseUrl + 'doctors/' + id);
  }

  public updateDoctor(user: User): Observable<any> {
    return this.http.put(baseUrl + updateUserApi, user);
  }
}
