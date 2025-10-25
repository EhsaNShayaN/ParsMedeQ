import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {BaseResult} from '../../models/BaseResult';
import {endpoint} from '../cookie-utils';
import {catchError} from 'rxjs/operators';
import {TicketResponse} from '../../models/TicketResponse';
import {BaseRestService} from './base-rest-service';

@Injectable({
  providedIn: 'root'
})
export class TicketService extends BaseRestService {
  addTicket(model: any, file: any): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('file', file);
    formData.append('model', JSON.stringify(model));
    return this.http.post<BaseResult<boolean>>(endpoint() + 'ticket/add', formData).pipe(
      catchError(this.handleError)
    );
  }

  addTicketAnswer(model: any, file: any): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('file', file);
    formData.append('model', JSON.stringify(model));
    return this.http.post<TicketResponse>(endpoint() + 'ticket/addAnswer', formData).pipe(
      catchError(this.handleError)
    );
  }

  getTicket(id: string): Observable<any> {
    const model = {id};
    return this.http.post<TicketResponse>(endpoint() + 'ticket/details', model).pipe(
      catchError(this.handleError)
    );
  }

  getTickets(model: any, url: string): Observable<any> {
    return this.http.post<TicketResponse>(`${endpoint()}${url}/ticket/list`, model).pipe(
      catchError(this.handleError)
    );
  }

  updateTicketStatus(id: number, status: number): Observable<any> {
    const model = {id, title: status};
    return this.http.post<BaseResult<boolean>>(endpoint() + 'admin/ticket/updateStatus', model).pipe(
      catchError(this.handleError)
    );
  }
}
