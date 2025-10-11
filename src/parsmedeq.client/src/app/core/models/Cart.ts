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

export interface Cart {
  id?: number;
  anonymousId?: string;
  items: CartItem[];
}
