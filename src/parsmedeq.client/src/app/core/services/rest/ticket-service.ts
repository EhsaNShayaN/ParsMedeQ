import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable, throwError} from 'rxjs';
import {BaseResult} from '../../models/BaseResult';
import {endpoint} from '../cookie-utils';
import {catchError} from 'rxjs/operators';
import {TicketResponse} from '../../models/TicketResponse';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  constructor(private http: HttpClient) {
  }

  addTicket(model: any, file: any): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('file', file);
    formData.append('model', JSON.stringify(model));
    return this.http.post<BaseResult<boolean>>(endpoint() + 'general/addTicket', formData).pipe(
      catchError(this.handleError)
    );
  }

  addTicketAnswer(model: any, file: any): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('file', file);
    formData.append('model', JSON.stringify(model));
    return this.http.post<TicketResponse>(endpoint() + 'general/addTicketAnswer', formData).pipe(
      catchError(this.handleError)
    );
  }

  getTicket(id: string): Observable<any> {
    const model = {id};
    return this.http.post<TicketResponse>(endpoint() + 'general/ticket', model).pipe(
      catchError(this.handleError)
    );
  }

  getTickets(model: any): Observable<any> {
    return this.http.post<TicketResponse>(`${endpoint()}role/tickets`, model).pipe(
      catchError(this.handleError)
    );
  }

  updateTicketStatus(id: number, status: number): Observable<any> {
    const model = {id, title: status};
    return this.http.post<BaseResult<boolean>>(endpoint() + 'admin/updateTicketStatus', model).pipe(
      catchError(this.handleError)
    );
  }

  handleError<T>(error: HttpErrorResponse): Observable<any> {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    return throwError('خطای نامشخص، لطفاً لحظاتی دیگر تلاش نمایید.');
  }
}
