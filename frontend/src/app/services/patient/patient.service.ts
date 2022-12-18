import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Doctor } from 'app/shared/models/doctor.model';
import { Office } from 'app/shared/models/office.model';
import { User } from 'app/shared/models/user.model';
import { catchError, map, Observable } from 'rxjs';
import { Appointment } from '../../shared/models/appointment.model';

const baseUrl = 'https://localhost:7221/';
const appointmentsApi: string = 'api/appointments';
const updateUserApi: string = 'api/users/update-user';
const userApi: string = 'api/users/';
const officesApi: string = 'api/offices'

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  constructor(private http: HttpClient) { }

  public getPatientById(id: string): Observable<User> {
    return this.http.get<User>(baseUrl + userApi + id);
  }

  public updatePatient(user: User): Observable<any>{
    return this.http.put(baseUrl+updateUserApi, user);
  }
}
