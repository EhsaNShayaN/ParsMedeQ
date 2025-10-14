import {Component, computed, OnInit, Signal} from '@angular/core';
import {CartService} from '../../core/services/cart.service';
import {Cart} from '../../core/models/Cart';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  standalone: false
})
export class CartComponent implements OnInit {
  cart!: Signal<Cart | null>; // or appropriate type
  total = computed(() => this.cart()?.cartItems.reduce((s, i) => s + i.unitPrice * i.quantity, 0) ?? 0);

  constructor(private cartService: CartService) {
    this.cart = this.cartService.cart;
  }

  ngOnInit() {
    this.cartService.loadCart(); // سبد ناشناس در شروع
  }

  removeItem(id: number) {
    this.cartService.removeFromCart(id);
  }

  checkout() {
    console.log('رفتن به مرحله بعد...');
  }

  payment() {
    console.log('رفتن به پرداخت...');
  }
}
