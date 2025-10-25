import {Injectable} from '@angular/core';
import {BaseRestService} from './base-rest-service';
import {Observable} from 'rxjs';
import {AddResult, BaseResult} from '../../models/BaseResult';
import {endpoint} from '../cookie-utils';
import {catchError} from 'rxjs/operators';
import {CommentResponse, CommentsRequest} from '../../models/CommentResponse';

@Injectable({
  providedIn: 'root'
})
export class CommentService extends BaseRestService {

  getComments(model: CommentsRequest, url:string): Observable<any> {
    return this.http.post<CommentResponse>(`${endpoint()}${url}/comment/list`, model).pipe(
      catchError(this.handleError)
    );
  }

  deleteComment(id: number): Observable<any> {
    const model = {id};
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}admin/comment/delete`, model).pipe(
      catchError(this.handleError)
    );
  }

  confirmComment(id: number, isConfirmed: boolean): Observable<any> {
    const model = {id, isConfirmed};
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}admin/comment/confirm`, model).pipe(
      catchError(this.handleError)
    );
  }

  addCommentAnswer(id: number, answer: string): Observable<any> {
    const model = {id, answer};
    return this.http.post<BaseResult<AddResult>>(`${endpoint()}admin/comment/addAnswer`, model).pipe(
      catchError(this.handleError)
    );
  }
}
