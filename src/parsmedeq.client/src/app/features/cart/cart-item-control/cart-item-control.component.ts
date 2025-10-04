import {Component, computed, Input, Signal} from '@angular/core';
import {CartService} from '../../../core/services/cart.service';
import {Cart, CartItem} from '../../../core/models/Cart';

@Component({
  selector: 'app-cart-item-control',
  templateUrl: './cart-item-control.component.html',
  styleUrl: './cart-item-control.component.scss',
  standalone: false
})
export class CartItemControlComponent {
  @Input() productId!: number;
  @Input() productType!: string;
  @Input() productName!: string;
  @Input() unitPrice!: number;
  @Input() stock!: number;   // 👈 موجودی محصول

  cart!: Signal<Cart | null>; // or appropriate type

  item = computed(() =>
    this.cart()?.items.find(i => i.productId === this.productId && i.productType === this.productType)
  );

  quantity = computed(() => this.item()?.quantity ?? 0);

  constructor(private cartService: CartService) {
    this.cart = this.cartService.cart;
  }

  add() {
    if (this.stock <= 0) {
      alert("این محصول ناموجود است");
      return;
    }
    if (this.quantity() >= this.stock) {
      alert(`حداکثر تعداد قابل سفارش ${this.stock} عدد است`);
      return;
    }

    const item: CartItem = {
      productId: this.productId,
      productType: this.productType,
      productName: this.productName,
      unitPrice: this.unitPrice,
      quantity: 1
    };
    this.cartService.addToCart(item, this.cart()?.userId);
  }

  remove() {
    if (this.item()) {
      this.cartService.removeFromCart(this.item()!.id!, this.cart()?.userId);
    }
  }

  increase() {
    if (this.quantity() < this.stock) {
      this.cartService.addToCart({...this.item()!, quantity: 1}, this.cart()?.userId);
    } else {
      alert(`حداکثر تعداد قابل سفارش ${this.stock} عدد است`);
    }
  }

  decrease() {
    const item = this.item();
    if (item && item.quantity > 1) {
      this.cartService.addToCart({...item, quantity: -1}, this.cart()?.userId);
    } else {
      this.remove();
    }
  }
}
