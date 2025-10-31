import {BaseResult} from './BaseResult';
import {AlborzPagingRequest, Paginated} from './Pagination';

export class OrdersRequest extends AlborzPagingRequest {
}

export interface OrdersResponse extends BaseResult<Paginated<Order>> {
}

export interface OrderResponse extends BaseResult<Order> {
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
  fullName: string;
  updateDate: string;
  creationDate: Date;
  items: OrderItem[];
}

export interface OrderItem {
  tableId: number;
  relatedId: number;
  relatedName: string;
  image: string;
  quantity: number;
  unitPrice: number;
  subtotal?: number | null;
  guarantyExpirationDate: string | null;
  periodicServiceInterval: number;
}

export interface AddOrderResponse extends BaseResult<AddOrder> {
}

export interface AddOrder {
  paymentId: number;
}
