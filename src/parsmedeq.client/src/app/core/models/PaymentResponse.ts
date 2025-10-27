/*import {Profile} from './UserResponse';*/
import {BaseResult} from './BaseResult';
import {AlborzPagingRequest} from './Pagination';

export class PaymentsRequest extends AlborzPagingRequest {
}

export interface PaymentResponse extends BaseResult<Payment> {
}

export interface Payment {
  Id: number;
  OrderId: number;
  Amount: number;
  PaymentMethod: number;
  TransactionId?: string | null;
  Status: number;
  PaidDate?: Date | null;
  CreationDate: Date;
}
