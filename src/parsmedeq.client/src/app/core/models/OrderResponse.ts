/*import {Profile} from './UserResponse';*/
import {BaseResult} from './BaseResult';
import {AlborzPagingRequest, Paginated} from './Pagination';

export class OrdersRequest extends AlborzPagingRequest {
}

export interface OrderResponse extends BaseResult<Paginated<Order>> {
}

export interface Order {
  id: number;
  userId: number;
  orderNumber: string;
  totalAmount: number;
  discountAmount: number;
  finalAmount?: number | null;
  status: number;
  statusText: string;
  updateDate?: Date | null;
  creationDate: Date;
}
