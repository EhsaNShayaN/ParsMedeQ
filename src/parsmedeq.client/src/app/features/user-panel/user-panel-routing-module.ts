import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {UserPanel} from './user-panel';
import {OrderComponent} from './orders/order/order.component';
import {UserCommentComponent} from './comment/user-comment.component';
import {UserCommentListComponent} from './comment/user-comment-list/user-comment-list.component';
import {UserTicketComponent} from './ticket/user-ticket.component';
import {UserTicketListComponent} from './ticket/user-ticket-list/user-ticket-list.component';

const routes: Routes = [
  {path: '', component: UserPanel},

  {path: 'order/:id', component: OrderComponent},

  {path: 'comment', component: UserCommentComponent},
  {path: 'comment/list', component: UserCommentListComponent},

  {path: 'ticket', component: UserTicketComponent},
  {path: 'ticket/list', component: UserTicketListComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserPanelRoutingModule {
}
