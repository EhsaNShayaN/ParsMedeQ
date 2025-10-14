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
  delta?: number;
  relatedId!: number;
  tableId!: number;
  relatedName!: string;
  unitPrice!: number;
  quantity!: number;
  originalQuantity!: number;

  constructor() {
    this.delta = this.quantity - this.originalQuantity;
  }
}
