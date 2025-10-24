import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {CartComponent} from './cart.component';
import {CartShippingComponent} from './cart-shipping/cart-shipping.component';
import {AuthGuard} from '../../core/guards/auth.guard';
import {CartFinishComponent} from './cart-finish/cart-finish.component';

const routes: Routes = [
  {path: '', component: CartComponent},
  {path: 'checkout', component: CartComponent},
  {path: 'shipping', component: CartShippingComponent, canActivate: [AuthGuard]},
  {path: 'finish/:id', component: CartFinishComponent, canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CartRoutingModule {
}
