import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {UserPanel} from './user-panel';
import {OrderComponent} from './orders/order/order.component';
import {UserTicketComponent} from './ticket/user-ticket.component';
import {UserTicketListComponent} from './ticket/ticket-list/user-ticket-list.component';

const routes: Routes = [
  {path: '', component: UserPanel},

  {path: 'order/:id', component: OrderComponent},

  {path: 'ticket', component: UserTicketComponent},
  {path: 'ticket/list', component: UserTicketListComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserPanelRoutingModule {
}
