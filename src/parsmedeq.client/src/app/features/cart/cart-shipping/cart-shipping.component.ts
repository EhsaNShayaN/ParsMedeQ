import {Component, computed, OnInit, Signal} from '@angular/core';
import {BaseComponent} from '../../../base-component';
import {Cart} from '../../../core/models/Cart';
import {CartService} from '../../../core/services/cart.service';
import {first} from 'rxjs/operators';
import {AddOrderResponse} from '../../../core/models/AddOrderResponse';
import {OrderService} from '../../../core/services/rest/order-service';

@Component({
  selector: 'app-cart-shipping',
  templateUrl: './cart-shipping.component.html',
  styleUrls: ['../cart.component.scss'],
  standalone: false
})
export class CartShippingComponent extends BaseComponent implements OnInit {
  cart!: Signal<Cart | null>; // or appropriate type
  total = computed(() => this.cart()?.cartItems.reduce((s, i) => s + i.unitPrice * i.quantity, 0) ?? 0);

  constructor(private cartService: CartService,
              private orderService: OrderService) {
    super();
    this.cart = this.cartService.cart;
  }

  ngOnInit() {
    this.cartService.loadCart(); // سبد ناشناس در شروع
  }

  payment() {
    this.orderService.addOrder(this.cart()?.id ?? 0).pipe(first()).subscribe((d: AddOrderResponse) => {
      const currentLang = this.translateService.getDefaultLang();
      this.navigateToLink(`/cart/finish/${d.data.paymentId}`, currentLang);
    });
  }
}
