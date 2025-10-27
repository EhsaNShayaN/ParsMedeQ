/*import {Profile} from './UserResponse';*/
import {BaseResult} from './BaseResult';
import {AlborzPagingRequest} from './Pagination';

export class PaymentsRequest extends AlborzPagingRequest {
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
  paidDate?: Date | null;
  creationDate: Date;
}
