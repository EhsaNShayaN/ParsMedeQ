import {BaseResult} from './BaseResult';

export interface AddOrderResponse extends BaseResult<AddOrder> {
}

export interface AddOrder {
  paymentId: number;
}
