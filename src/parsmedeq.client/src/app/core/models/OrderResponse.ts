/*import {Profile} from './UserResponse';*/
import {BaseResult} from './BaseResult';
import {AlborzPagingRequest, Paginated} from './Pagination';

export class OrdersRequest extends AlborzPagingRequest {
}

export interface OrderResponse extends BaseResult<Paginated<Order>> {
}

export interface Order {
  Id: number;
  UserId: number;
  OrderNumber: string;
  TotalAmount: number;
  DiscountAmount: number;
  FinalAmount?: number | null;
  Status: number;
  UpdateDate?: Date | null;
  CreationDate: Date;
}
