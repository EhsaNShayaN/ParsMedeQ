/*import {Profile} from './UserResponse';*/
import {BaseResult} from './BaseResult';
import {PagingRequest} from './Pagination';

export class PaymentsRequest extends PagingRequest {
}

export interface PaymentResponse extends BaseResult<Payment> {
}

export interface Payment {
  id: number;
  amount: number;
  paymentMethod: number;
  transactionId?: string | null;
  status: number;
  statusText: string;
  fullName: string;
  paidDate?: Date | null;
  creationDate: Date;
}
