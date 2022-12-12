import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Appointment } from 'app/shared/models/appointment.model';
import { Office } from 'app/shared/models/office.model';
import { User } from 'app/shared/models/user.model';
import { Observable } from 'rxjs';

const baseUrl = 'https://localhost:7221/';
const appointmentsApi: string = 'api/appointments';
const officesApi: string = 'api/offices'
const updateUserApi: string = 'api/users/update-user';
const userApi: string = 'api/users/';


@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  constructor(private http: HttpClient) { }


  public getAppointmentsByDoctorId(id: string): Observable<Appointment[]> {
    console.log(id)
    return this.http.get<Appointment[]>(baseUrl + appointmentsApi + '/doctor/' + id);
  }

  public getAllOffices(): Observable<Office[]> {
    return this.http.get<Office[]>(baseUrl + officesApi);
  }

  public assignDoctorToOffice(doctorId: string, officeId: string): Observable<any> {
    return this.http.put<any>(baseUrl + "api/users/assign-doctor-to-office/" + doctorId + "/" + officeId, { doctorId, officeId });
  }

  public getPatientById(id: string): Observable<User> {
    return this.http.get<User>(baseUrl + 'api/users/' + id);
  }

  public updateAppointment(id: string, appointment: Appointment): Observable<any> {
    return this.http.put<any>(baseUrl + 'api/appointments/' + id, appointment);
  }

  public getPatientByPatientId(id: string): Observable<User> {
    console.log(id);
    return this.http.get<User>(baseUrl + userApi + id);
  }

  public updatePatient(user: User): Observable<any>{
    console.log("User to update: " + user);
    return this.http.put(baseUrl+updateUserApi, user);
  }
}
