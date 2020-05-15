import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Assignment } from '../models/assignment';
import { Observable } from 'rxjs';
import { Payment } from '../models/payment';

@Injectable({
  providedIn: 'root'
})
export class AssignmentsService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  saveAssignmentDetails(job: Assignment) {
    return this.http.post(this.baseUrl + 'api/assignments', job);
  }

  getAssignmentDetails(userId: string): Observable <Assignment[]> {
    const options = userId ? {
      params: new HttpParams().set('id', userId)
    } : {};
    return this.http.get <Assignment[]> (this.baseUrl + 'api/assignments/GetAssignmentsByUserId', options).pipe();
  }

  savePayments(payment: Payment) {
    return this.http.post(this.baseUrl + 'api/assignments/SavePayment', payment);
  }

  getPaymentDetails(userId: string): Observable <Payment[]> {
    const options = userId ? {
      params: new HttpParams().set('id', userId)
    } : {};
    return this.http.get <Payment[]> (this.baseUrl + 'api/assignments/GetPaymentsByUserId', options).pipe();
  }

}
