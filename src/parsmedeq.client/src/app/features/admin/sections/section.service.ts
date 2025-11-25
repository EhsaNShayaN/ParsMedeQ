import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Section} from './homepage-sections.component';
import {Observable} from 'rxjs';
import {endpoint} from '../../../core/services/cookie-utils';
import {BaseResult} from '../../../core/models/BaseResult';

@Injectable({providedIn: 'root'})
export class SectionService {
  private api = `${endpoint()}admin/section`;

  constructor(private http: HttpClient) {
  }

  getAll(): Observable<BaseResult<Section[]>> {
    return this.http.get<BaseResult<Section[]>>(`${this.api}/list`);
  }

  update(id: number, data: any) {
    return this.http.post(`${this.api}/edit/${id}`, data);
  }

  toggle(id: number, hide: boolean) {
    if (hide) {
      return this.http.post(`${this.api}/show`, {id});
    }
    return this.http.post(`${this.api}/hide`, {id});
  }

  updateOrder(list: { id: number, order: number }[]) {
    return this.http.post(`${this.api}/order`, list);
  }

  uploadImage(file: File) {
    const fd = new FormData();
    fd.append('file', file);
    return this.http.post<{ url: string }>(`${this.api}/upload`, fd);
  }
}
