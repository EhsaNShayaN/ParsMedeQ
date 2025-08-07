import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {WeatherForecast} from '../models/WeatherForecast';
import {endpoint} from './services/cookie-utils';

@Injectable({
  providedIn: 'root'
})
export class RestApiService {
  constructor(private http: HttpClient) {
  }

  getWeatherForecast(): Observable<any> {
    return this.http.get<WeatherForecast[]>(`${endpoint()}weatherForecast`).pipe(
      catchError(this.handleError)
    );
  }

  handleError(error: HttpErrorResponse): any {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}, ` + `body was: ${error.error}`);
    }
    return throwError('خطای نامشخص، لطفاً لحظاتی دیگر تلاش نمایید.');
  }
}
