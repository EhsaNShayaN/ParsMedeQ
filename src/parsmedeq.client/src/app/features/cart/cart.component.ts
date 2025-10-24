import {Component, computed, OnInit, Signal} from '@angular/core';
import {BaseComponent} from '../../base-component';
import {Cart} from '../../core/models/Cart';
import {CartService} from '../../core/services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
  standalone: false
})
export class CartComponent extends BaseComponent implements OnInit {
  cart!: Signal<Cart | null>; // or appropriate type
  total = computed(() => this.cart()?.cartItems.reduce((s, i) => s + i.unitPrice * i.quantity, 0) ?? 0);

  constructor(private cartService: CartService) {
    super();
    this.cart = this.cartService.cart;
  }

  ngOnInit() {
    this.cartService.loadCart(); // سبد ناشناس در شروع
  }

  shipping() {
  }
}
