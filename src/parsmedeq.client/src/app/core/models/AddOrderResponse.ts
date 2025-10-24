import {BaseResult} from './BaseResult';

export interface OrderResponse extends BaseResult<OrderResult> {

}

export interface OrderResult {
  paymentId: number;
}
