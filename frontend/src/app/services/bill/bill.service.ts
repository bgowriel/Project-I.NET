import {
  HttpClient,
  HttpErrorResponse,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Doctor } from 'app/shared/models/doctor.model';
import { User } from 'app/shared/models/user.model';
import { catchError, map, Observable } from 'rxjs';
import { Bill } from '../../shared/models/bill.model';

const baseUrl = 'https://localhost:7221/';
const billsApi: string = 'api/bills';

@Injectable({
  providedIn: 'root',
})
export class BillService {
  constructor(private http: HttpClient) {}

  public getBills(): Observable<Bill[]> {
    return this.http.get<Bill[]>(baseUrl + billsApi);
  }

  public getBillById(id: string): Observable<Bill[]> {
    return this.http.get<Bill[]>(baseUrl + billsApi + id);
  }

  public getBillsByPatientId(id: string): Observable<Bill[]> {
    return this.http.get<Bill[]>(baseUrl + billsApi + '/patient/' + id);
  }

  public getBillsByDoctorId(id: string): Observable<Bill[]> {
    return this.http.get<Bill[]>(baseUrl + billsApi + '/doctor/' + id);
  }

  public createBill(bill: Bill): Observable<any> {
    return this.http.post(baseUrl + billsApi, bill);
  }

  public deleteBill(id: string): Observable<any> {
    return this.http.delete<any>(baseUrl + billsApi + id);
  }
}
