import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Appointment } from 'app/shared/models/appointment.model';
import { Doctor } from 'app/shared/models/doctor.model';
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
export class OfficeService {

  constructor(private http: HttpClient) { }

  public getAllOffices(): Observable<Office[]> {
    return this.http.get<Office[]>(baseUrl + officesApi);
  }

  public assignDoctorToOffice(doctorId: string, officeId: string): Observable<any> {
    return this.http.put<any>(baseUrl + "api/users/assign-doctor-to-office/" + doctorId + "/" + officeId, { doctorId, officeId });
  }

  public getOfficeById(id: string): Observable<Doctor> {
    return this.http.get<Doctor>(baseUrl + 'api/offices/' + id);
  }

  public registerOffice(office: Office): Observable<any> {
    return this.http.post(baseUrl + 'api/offices', office);
  }

  public updateOffice(id: string, office: Office): Observable<any> {
    return this.http.put<any>(baseUrl + 'api/offices/' + id, office);
  }

  public deleteOffice(id: string): Observable<any> {
    return this.http.delete<any>(baseUrl + 'api/offices/' + id);
  }
}
