import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SharedModule} from '../../shared/shared-module';
import {CartComponent} from './cart.component';
import {CartRoutingModule} from './cart-routing-module';
import {CartShippingComponent} from './cart-shipping/cart-shipping.component';
import {CartFinishComponent} from './cart-finish/cart-finish.component';

@NgModule({
  declarations: [
    CartComponent,
    CartShippingComponent,
    CartFinishComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    CartRoutingModule,
  ]
})
export class CartModule {
}
