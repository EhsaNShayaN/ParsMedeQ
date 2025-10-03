export interface CartItem {
  id?: string;
  productId: string;
  productType: string;
  productName: string;
  unitPrice: number;
  quantity: number;
}

export interface Cart {
  id?: string;
  userId?: string;
  anonymousId?: string;
  items: CartItem[];
}
