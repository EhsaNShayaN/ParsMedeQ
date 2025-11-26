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

  getAllItems(): Observable<BaseResult<Section[]>> {
    return this.http.get<BaseResult<Section[]>>(`${this.api}/items`);
  }

  update0(id: number, data: any) {
    return this.http.post(`${this.api}/update`, data);
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

  update(id: number, title: string, description: string, oldImagePath?: string, image?: File) {
    const fd = new FormData();
    fd.append('id', id.toString());
    fd.append('title', title);
    fd.append('description', description);
    fd.append('oldImagePath', oldImagePath ?? '');
    if (image) {
      fd.append('image', image);
    }
    return this.http.post<{ url: string }>(`${this.api}/update`, fd);
  }

  updateByList(model: UpdateListRequest) {
    return this.http.post(`${this.api}/updateByList`, model);
  }

  deleteImage(id: number) {
    return this.http.post(`${this.api}/deleteImage`, {id});
  }
}

export interface UpdateListRequest {
  id: number;
  items: UpdateListItem[];
}

export interface UpdateListItem {
  title: string;
  description: string;
  image: string;
}
