import {BaseResult} from "./BaseResult";

export interface CartResponse extends BaseResult<Cart> {
}

export interface Cart {
  id?: number;
  anonymousId?: string;
  cartItems: CartItem[];
}

export class CartItem {
  id?: number;
  tableId!: number;
  relatedId!: number;
  relatedName!: string;
  image?: string;
  unitPrice!: number;
  quantity!: number;
}
