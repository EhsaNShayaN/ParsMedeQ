import {Injectable} from '@angular/core';
import {BaseRestService} from './base-rest-service';
import {Observable} from 'rxjs';
import {AddResult, BaseResult} from '../../models/BaseResult';
import {endpoint} from '../cookie-utils';
import {catchError} from 'rxjs/operators';
import {OrderResponse, OrdersRequest, OrdersResponse} from '../../models/OrderResponse';
import {AddOrderResponse} from '../../models/OrderResponse';

@Injectable({
  providedIn: 'root'
})
export class OrderService extends BaseRestService {

  getOrders(model: OrdersRequest, url: string): Observable<any> {
    return this.http.post<OrdersResponse>(`${endpoint()}${url}/order/list`, model).pipe(
      catchError(this.handleError)
    );
  }

  deleteOrder(id: number): Observable<any> {
    const model = {id};
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}admin/order/delete`, model).pipe(
      catchError(this.handleError)
    );
  }

  confirmOrder(id: number, isConfirmed: boolean): Observable<any> {
    const model = {id, isConfirmed};
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}admin/order/confirm`, model).pipe(
      catchError(this.handleError)
    );
  }

  addOrder(cartId: number): Observable<any> {
    const model = {cartId};
    return this.http.post<AddOrderResponse>(`${endpoint()}order/add`, model).pipe(
      catchError(this.handleError)
    );
  }

  getOrder(id: number): Observable<any> {
    return this.http.get<OrderResponse>(`${endpoint()}order/details?id=${id}`).pipe(
      catchError(this.handleError)
    );
  }
}
