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
  @Input() relatedId!: number;
  @Input() tableId!: number;
  @Input() relatedName!: string;
  @Input() unitPrice!: number;
  @Input() stock!: number;   // 👈 موجودی محصول

  cart!: Signal<Cart | null>; // or appropriate type

  item = computed(() =>
    this.cart()?.items.find(i => i.relatedId === this.relatedId && i.tableId === this.tableId)
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
      relatedId: this.relatedId,
      tableId: this.tableId,
      relatedName: this.relatedName,
      unitPrice: this.unitPrice,
      quantity: 1,
      originalQuantity: 0
    };
    this.cartService.addToCart(item);
  }

  remove() {
    if (this.item()) {
      this.cartService.removeFromCart(this.item()!.id!);
    }
  }

  increase() {
    if (this.quantity() < this.stock) {
      this.cartService.addToCart({...this.item()!, quantity: 1});
    } else {
      alert(`حداکثر تعداد قابل سفارش ${this.stock} عدد است`);
    }
  }

  decrease() {
    const item = this.item();
    if (item && item.quantity > 1) {
      this.cartService.addToCart({...item, quantity: -1});
    } else {
      this.remove();
    }
  }
}
