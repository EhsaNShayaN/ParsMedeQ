/*import {Profile} from './UserResponse';*/
import {AlborzPagingRequest, Paginated} from './Pagination';

export class CommentsRequest extends AlborzPagingRequest {
}

export interface CommentResponse {
  data: Paginated<Comment>;
}

export interface Comment {
  id: number;
  icon: string;
  description: string;
  relatedId: string;
  tableId: number;
  tableName: string;
  data: string;
  answers: string[];
  isConfirmed: boolean;
  creationDate: string;
  profile: any;
}
