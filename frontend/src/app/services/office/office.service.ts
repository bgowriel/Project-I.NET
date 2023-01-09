import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Appointment } from 'app/shared/models/appointment.model';
import { Doctor } from 'app/shared/models/doctor.model';
import { Office } from 'app/shared/models/office.model';
import { User } from 'app/shared/models/user.model';
import { Observable } from 'rxjs';

const baseUrl = 'https://localhost:7221/';
const appointmentsApi: string = 'api/v1/appointments';
const officesApi: string = 'api/v1/offices'
const updateUserApi: string = 'api/v1/users/update-user';
const userApi: string = 'api/v1/users/';

@Injectable({
  providedIn: 'root'
})
export class OfficeService {

  constructor(private http: HttpClient) { }

  public getAllOffices(): Observable<Office[]> {
    return this.http.get<Office[]>(baseUrl + officesApi);
  }

  public assignDoctorToOffice(doctorId: string, officeId: string): Observable<any> {
    return this.http.put<any>(baseUrl + "api/v1/users/assign-doctor-to-office/" + doctorId + "/" + officeId, { doctorId, officeId });
  }

  public getOfficeById(id: string): Observable<Doctor> {
    return this.http.get<Doctor>(baseUrl + 'api/v1/offices/' + id);
  }

  public registerOffice(office: Office): Observable<any> {
    return this.http.post(baseUrl + 'api/v1/offices', office);
  }

  public updateOffice(id: string, office: Office): Observable<any> {
    return this.http.put<any>(baseUrl + 'api/v1/offices/' + id, office);
  }

  public deleteOffice(id: string): Observable<any> {
    return this.http.delete<any>(baseUrl + 'api/v1/offices/' + id);
  }
}
