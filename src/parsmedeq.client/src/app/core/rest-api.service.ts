import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {map, Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {endpoint} from './services/cookie-utils';
import {AddResourceRequest, Resource, ResourceRequest, ResourceResponse, ResourcesRequest} from './models/ResourceResponse';
import {BaseResult} from './models/BaseResult';
import {ResourceCategoriesResponse} from './models/ResourceCategoryResponse';
import {AuthService} from './services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class RestApiService {
  constructor(private http: HttpClient,
              private authService: AuthService) {
  }

  getResourceCategories(tableId: number): Observable<any> {
    return this.http.get<ResourceCategoriesResponse>(`${endpoint()}resource/category/list?tableId=${tableId}`,).pipe(
      catchError(this.handleError)
    );
  }

  getResource(model: ResourceRequest): Observable<any> {
    const query = this.modelToQuery(model);
    return this.http.get<BaseResult<Resource>>(`${endpoint()}resource/details?${query}`).pipe(
      catchError(this.handleError)
    );
  }

  getResources(model: ResourcesRequest): Observable<any> {
    const query = this.modelToQuery(model);
    return this.http.get<ResourceResponse>(`${endpoint()}resource/list?${query}`).pipe(
      catchError(this.handleError)
    );
  }

  addResourceCategory(model: any): Observable<any> {
    return this.http.post<BaseResult<boolean>>(endpoint() + 'resource/category/add', model).pipe(
      catchError(this.handleError)
    );
  }

  editResourceCategory(model: any): Observable<any> {
    return this.http.post<BaseResult<boolean>>(endpoint() + 'resource/category/update', model).pipe(
      catchError(this.handleError)
    );
  }

  addResource(model: AddResourceRequest, image: any = null, file: any = null): Observable<any> {
    const formData: FormData = this.toFormData(model);
    //const formData: FormData = new FormData();
    formData.append('image', image);
    formData.append('file', file);
    //formData.append('model', JSON.stringify(model));
    return this.http.post<BaseResult<boolean>>(`${endpoint()}resource/${model.id ? 'edit' : 'add'}`, formData).pipe(
      catchError(this.handleError)
    );
  }

  modelToQuery(model: any) {
    let params = new URLSearchParams(model);
    let keysForDel: string[] = [];
    params.forEach((value, key) => {
      if (!value || value === 'null' || value === 'undefined') {
        keysForDel.push(key);
      }
    });
    keysForDel.forEach(key => {
      params.delete(key);
    });
    return params.toString();
  }

  toFormData<T extends Record<string, any>>(obj: T): FormData {
    const formData = new FormData();

    Object.entries(obj).forEach(([key, value]) => {

      console.log(key, typeof (value));

      if (value === undefined || value === null) return;

      // If it's a File or Blob -> append directly (binary)
      if (value instanceof File || value instanceof Blob) {
        formData.append(key, value);
      }
      // If it's an array
      else if (Array.isArray(value)) {
        value.forEach((item, i) => {
          if (item instanceof File || item instanceof Blob) {
            formData.append(`${key}[${i}]`, item);
          } else if (typeof item === 'object') {
            formData.append(`${key}[${i}]`, JSON.stringify(item));
          } else {
            formData.append(`${key}[${i}]`, String(item));
          }
        });
      }
      // If it's an object (non-binary)
      else if (typeof value === 'object') {
        formData.append(key, JSON.stringify(value));
      }
      // primitives
      else {
        formData.append(key, String(value));
      }
    });

    return formData;
  }

  logout(): Observable<any> {
    return this.http.post<BaseResult<boolean>>(`${endpoint()}resource/logout`, null)
      .pipe(catchError(this.handleError),
        map((d: BaseResult<boolean>) => {
          this.authService.logout();
          return d.data;
        }));
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
