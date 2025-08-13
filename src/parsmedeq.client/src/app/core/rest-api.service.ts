import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {endpoint} from './services/cookie-utils';
import {WeatherForecast} from './models/WeatherForecast';
import {AddResourceRequest, Resource, ResourceRequest, ResourceResponse, ResourcesRequest} from './models/ResourceResponse';
import {BaseResult} from './models/BaseResult';
import {ResourceCategoriesResponse} from './models/ResourceCategoryResponse';

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

  getResourceCategories(tableId: number): Observable<any> {
    const model = {id: tableId};
    return this.http.post<ResourceCategoriesResponse>(`${endpoint()}general/resourceCategories`, model).pipe(
      catchError(this.handleError)
    );
  }

  getResource(model: ResourceRequest): Observable<any> {
    return this.http.post<Resource>(`${endpoint()}general/resource`, model).pipe(
      catchError(this.handleError)
    );
  }

  getResources(model: ResourcesRequest): Observable<any> {
    return this.http.post<ResourceResponse>(`${endpoint()}general/resources`, model).pipe(
      catchError(this.handleError)
    );
  }

  addResourceCategory(model: any): Observable<any> {
    return this.http.post<BaseResult<boolean>>(endpoint() + 'admin/addResourceCategory', model).pipe(
      catchError(this.handleError)
    );
  }

  addResource0(): Observable<any> {
    const model = {mobile: '9123440731'};
    return this.http.post<any>(`${endpoint()}resource/add`, model).pipe(
      catchError(this.handleError)
    );
  }

  addResource(model: AddResourceRequest, image: any = null, file: any = null): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('image', image);
    formData.append('file', file);
    formData.append('model', JSON.stringify(model));
    return this.http.post<BaseResult<boolean>>(endpoint() + 'admin/addResource', formData).pipe(
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
