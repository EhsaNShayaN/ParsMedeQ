import {Injectable} from '@angular/core';
import {BaseRestService} from './base-rest-service';
import {Observable} from 'rxjs';
import {AddResult, BaseResult} from '../../models/BaseResult';
import {endpoint} from '../cookie-utils';
import {catchError} from 'rxjs/operators';
import {PaymentResponse, PaymentsRequest} from '../../models/PaymentResponse';
import {ConfirmPaymentResponse} from '../../models/ConfirmPaymentResponse';

@Injectable({
  providedIn: 'root'
})
export class PaymentService extends BaseRestService {

  getPayments(model: PaymentsRequest, url: string): Observable<any> {
    return this.http.post<PaymentResponse>(`${endpoint()}${url}/payment/list`, model).pipe(
      catchError(this.handleError)
    );
  }

  deletePayment(id: number): Observable<any> {
    const model = {id};
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}admin/payment/delete`, model).pipe(
      catchError(this.handleError)
    );
  }

  addPayment(model: any): Observable<any> {
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}payment/add`, model).pipe(
      catchError(this.handleError)
    );
  }

  confirmPayment(paymentId: number, transactionId: string): Observable<any> {
    const model = {paymentId, transactionId};
    return this.http.post<ConfirmPaymentResponse>(`${endpoint()}payment/confirm`, model).pipe(
      catchError(this.handleError)
    );
  }

  failPayment(model: any): Observable<any> {
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}payment/fail`, model).pipe(
      catchError(this.handleError)
    );
  }
}
