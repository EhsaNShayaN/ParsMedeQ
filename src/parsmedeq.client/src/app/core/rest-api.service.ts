import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {map, Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {endpoint} from './services/cookie-utils';
import {AddResourceRequest, Resource, ResourceRequest, ResourceResponse, ResourcesRequest} from './models/ResourceResponse';
import {AddResult, BaseResult} from './models/BaseResult';
import {ResourceCategoriesResponse} from './models/ResourceCategoryResponse';
import {AuthService} from './services/auth.service';
import {ProductCategoriesResponse} from './models/ProductCategoryResponse';
import {AddProductRequest, Product, ProductRequest, ProductResponse, ProductsRequest} from './models/ProductResponse';
import {MobileRequest, MobileResponse, ProfileResponse, SendOtpRequest, SendOtpResponse} from './models/Login';
import {ProductMediaListResponse} from './models/ProductMediaResponse';
import {LocationResponse} from './models/LocationResponse';
import {TreatmentCenter, TreatmentCenterResponse} from './models/TreatmentCenterResponse';

@Injectable({
  providedIn: 'root'
})
export class RestApiService {
  constructor(private http: HttpClient,
              private authService: AuthService) {
  }

  sendOtp(model: SendOtpRequest): Observable<any> {
    return this.http.post<BaseResult<SendOtpResponse>>(`${endpoint()}user/signIn/mobile/sendotp`, model).pipe(
      catchError(this.handleError)
    );
  }

  sendMobile(model: MobileRequest): Observable<any> {
    return this.http.post<BaseResult<MobileResponse>>(`${endpoint()}user/signIn/mobile`, model).pipe(
      catchError(this.handleError)
    );
  }

  getUserInfo(): Observable<any> {
    return this.http.get<ProfileResponse>(`${endpoint()}user/details`).pipe(
      catchError(this.handleError)
    );
  }

  setPassword(password: string): Observable<any> {
    const model: any = {password};
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}user/setPassword`, model).pipe(
      catchError(this.handleError)
    );
  }

  changePassword(oldPassword: string, password: string): Observable<any> {
    const model: any = {oldPassword, password};
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}user/changePassword`, model).pipe(
      catchError(this.handleError)
    );
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
    return this.http.post<BaseResult<boolean>>(`${endpoint()}resource/category/${model.id ? 'edit' : 'add'}`, model).pipe(
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

  deleteResource(id: number): Observable<any> {
    const model = {id};
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}resource/delete`, model).pipe(
      catchError(this.handleError)
    );
  }

  deleteResourceCategory(id: number): Observable<any> {
    const model = {id};
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}resource/category/delete`, model).pipe(
      catchError(this.handleError)
    );
  }

  deleteProduct(id: number): Observable<any> {
    const model = {id};
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}product/delete`, model).pipe(
      catchError(this.handleError)
    );
  }

  deleteProductCategory(id: number): Observable<any> {
    const model = {id};
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}product/category/delete`, model).pipe(
      catchError(this.handleError)
    );
  }

  ////////////////////////

  getProductCategories(): Observable<any> {
    return this.http.get<ProductCategoriesResponse>(`${endpoint()}product/category/list`,).pipe(
      catchError(this.handleError)
    );
  }

  getProductMediaList(productId: number): Observable<any> {
    return this.http.get<ProductMediaListResponse>(`${endpoint()}product/media/list?productId=${productId}`,).pipe(
      catchError(this.handleError)
    );
  }

  deleteProductMedia(model: any): Observable<any> {
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}product/media/delete`, model).pipe(
      catchError(this.handleError)
    );
  }

  addProductMedia(productId: number, files: File[]): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('productId', productId.toString());
    for (const file of files) {
      formData.append('files', file);
    }
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}product/media/add`, formData).pipe(
      catchError(this.handleError)
    );
  }

  getProduct(model: ProductRequest): Observable<any> {
    const query = this.modelToQuery(model);
    return this.http.get<BaseResult<Product>>(`${endpoint()}product/details?${query}`).pipe(
      catchError(this.handleError)
    );
  }

  getProducts(model: ProductsRequest): Observable<any> {
    const query = this.modelToQuery(model);
    return this.http.get<ProductResponse>(`${endpoint()}product/list?${query}`).pipe(
      catchError(this.handleError)
    );
  }

  addProductCategory(model: any, image: any = null): Observable<any> {
    const formData: FormData = this.toFormData(model);
    formData.append('image', image);
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}product/category/${model.id ? 'edit' : 'add'}`, formData).pipe(
      catchError(this.handleError)
    );
  }

  addProduct(model: AddProductRequest, image: any = null, file: any = null): Observable<any> {
    const formData: FormData = this.toFormData(model);
    //const formData: FormData = new FormData();
    formData.append('image', image);
    formData.append('file', file);
    //formData.append('model', JSON.stringify(model));
    return this.http.post<BaseResult<boolean>>(`${endpoint()}product/${model.id ? 'edit' : 'add'}`, formData).pipe(
      catchError(this.handleError)
    );
  }

  download(id: number, tableId: number, model: any = null): Observable<any> {
    const headers = new HttpHeaders({
      'ngsw-bypass': 'true'
    });
    let url = `${endpoint()}general/download?id=${id}&tableId=${tableId}`;
    if (model) {
      url += '&model=' + model;
    }
    return this.http.get(url, {
      headers,
      reportProgress: true,
      observe: 'events',
      responseType: 'arraybuffer'
    });
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
      if (value === undefined || value === null) return;

      if (value instanceof File || value instanceof Blob) {
        formData.append(key, value);
      } else if (Array.isArray(value)) {
        // Append the entire array as a JSON string
        formData.append(key, JSON.stringify(value));
      } else if (typeof value === 'object') {
        formData.append(key, JSON.stringify(value));
      } else {
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

  getLocations(): Observable<any> {
    return this.http.get<LocationResponse>(`${endpoint()}location/list`).pipe(
      catchError(this.handleError)
    );
  }

  getTreatmentCenters(model: any): Observable<any> {
    return this.http.post<TreatmentCenterResponse>(`${endpoint()}treatmentCenter/list`, model).pipe(
      catchError(this.handleError)
    );
  }

  getTreatmentCenter(id: number): Observable<any> {
    return this.http.get<BaseResult<TreatmentCenter>>(`${endpoint()}treatmentCenter/details?id=${id}`).pipe(
      catchError(this.handleError)
    );
  }

  addTreatmentCenter(model: any, image: any = null): Observable<any> {
    const formData: FormData = this.toFormData(model);
    formData.append('image', image);
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}treatmentCenter/${model.id ? 'edit' : 'add'}`, formData).pipe(
      catchError(this.handleError)
    );
  }

  deleteTreatmentCenter(id: number): Observable<any> {
    const model = {id};
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}treatmentCenter/delete`, model).pipe(
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
