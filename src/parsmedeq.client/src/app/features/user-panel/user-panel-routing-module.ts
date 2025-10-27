import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {UserPanel} from './user-panel';
import {UserCommentComponent} from './comment/user-comment.component';
import {UserCommentListComponent} from './comment/user-comment-list/user-comment-list.component';
import {UserTicketComponent} from './ticket/user-ticket.component';
import {UserTicketListComponent} from './ticket/user-ticket-list/user-ticket-list.component';
import {UserOrderListComponent} from './order/user-order-list/user-order-list.component';
import {UserPaymentListComponent} from './payment/user-payment-list/user-payment-list.component';
import {UserOrderComponent} from './order/user-order.component';
import {UserPaymentComponent} from './payment/user-payment.component';

const routes: Routes = [
  {path: '', component: UserPanel},

  {path: 'comment', component: UserCommentComponent},
  {path: 'comment/list', component: UserCommentListComponent},

  {path: 'ticket', component: UserTicketComponent},
  {path: 'ticket/list', component: UserTicketListComponent},

  {path: 'order', component: UserOrderComponent},
  {path: 'order/list', component: UserOrderListComponent},

  {path: 'payment', component: UserPaymentComponent},
  {path: 'payment/list', component: UserPaymentListComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserPanelRoutingModule {
}
