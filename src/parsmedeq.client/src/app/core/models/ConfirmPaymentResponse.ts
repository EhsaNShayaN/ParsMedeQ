import {BaseResult} from './BaseResult';

export interface ConfirmPaymentResponse extends BaseResult<ConfirmPayment> {
}

export interface ConfirmPayment {
  transactionId: string;
  orderId: number;
  orderNumber: string;
  amount: number;
}
