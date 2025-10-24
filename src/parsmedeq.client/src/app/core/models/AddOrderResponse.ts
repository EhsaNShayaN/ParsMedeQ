import {BaseResult} from './BaseResult';
import {OrderStatus} from '../constants/server.constants';

export interface AddOrderResponse extends BaseResult<AddOrder> {
}

export interface AddOrder {
  paymentId: number;
}

export interface OrderResponse extends BaseResult<Order> {
}

export interface Order {
  id: number;
  paymentId: number;
  orderNumber: string;
  totalAmount: number;
  discountAmount: number;
  finalAmount?: number | null;
  status: OrderStatus;
  updateDate?: string | null;   // یا Date اگر در تبدیل JSON مدیریت شود
  creationDate: string;         // یا Date
  items: OrderItem[];
}

export interface OrderItem {
  orderId: number;
  tableId: number;
  relatedId: number;
  relatedName: string;
  image: string;
  quantity: number;
  unitPrice: number;
  subtotal?: number | null;
}
