export class CartItem {
  id?: number;
  delta?: number;
  productId!: number;
  productType!: string;
  productName!: string;
  unitPrice!: number;
  quantity!: number;
  originalQuantity!: number;

  constructor() {
    this.delta = this.quantity - this.originalQuantity;
  }
}

export interface Cart {
  id?: number;
  userId?: string;
  anonymousId?: string;
  items: CartItem[];
}
