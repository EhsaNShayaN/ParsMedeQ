import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {UserPanel} from './user-panel';
import {OrderComponent} from './orders/order/order.component';

const routes: Routes = [
  {path: '', component: UserPanel},
  {path: 'order/:id', component: OrderComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserPanelRoutingModule {
}
