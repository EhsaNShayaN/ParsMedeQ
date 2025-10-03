import {Component, computed} from '@angular/core';
import {CartService} from '../../core/services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html'
})
export class CartComponent {
  cart = this.cartService.cart;
  total = computed(() => this.cart()?.items.reduce((s, i) => s + i.unitPrice * i.quantity, 0) ?? 0);

  constructor(private cartService: CartService) {
  }

  ngOnInit() {
    this.cartService.loadCart(); // سبد ناشناس در شروع
  }

  removeItem(id: string) {
    this.cartService.removeFromCart(id);
  }

  checkout() {
    console.log('رفتن به مرحله بعد...');
  }

  payment() {
    console.log('رفتن به پرداخت...');
  }
}
