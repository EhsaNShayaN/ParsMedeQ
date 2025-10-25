import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {UserPanelRoutingModule} from './user-panel-routing-module';
import {UserPanel} from './user-panel';
import {Dashboard} from './dashboard/dashboard';
import {Orders} from './orders/order-list/orders';
import {Warranty} from './warranty/warranty';
import {Service} from './service/service';
import {Repair} from './repair/repair';
import {Invoice} from './invoice/invoice';
import {SharedModule} from '../../shared/shared-module';
import {OrderComponent} from './orders/order/order.component';
import {UserCommentComponent} from './comment/user-comment.component';
import {UserCommentListComponent} from './comment/user-comment-list/user-comment-list.component';
import {UserTicketComponent} from './ticket/user-ticket.component';
import {UserTicketListComponent} from './ticket/user-ticket-list/user-ticket-list.component';
import {PanelsModule} from '../../shared/panels-component/panels-module';

@NgModule({
  declarations: [
    UserPanel,
    Dashboard,
    Orders,
    OrderComponent,
    Warranty,
    Service,
    Repair,
    Invoice,

    UserCommentComponent,
    UserCommentListComponent,

    UserTicketComponent,
    UserTicketListComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    PanelsModule,
    UserPanelRoutingModule
  ]
})
export class UserPanelModule {
}
