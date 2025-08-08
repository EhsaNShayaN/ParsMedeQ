/*import {Profile} from './UserResponse';*/
import {Paginated} from './Pagination';

export interface CommentResponse extends Paginated {
  data: Comment[];
  totalCount: number;
}

export interface Comment {
  id: string;
  icon: string;
  description: string;
  relatedId: string;
  tableId: number;
  tableName: string;
  data: string;
  answers: string[];
  isConfirmed: boolean;
  creationDate: string;
  //profile: Profile;
}
