export interface CartItem {
  id?: number;
  productId: number;
  productType: string;
  productName: string;
  unitPrice: number;
  quantity: number;
}

export interface Cart {
  id?: number;
  userId?: string;
  anonymousId?: string;
  items: CartItem[];
}
