import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {UserPanelRoutingModule} from './user-panel-routing-module';
import {UserPanel} from './user-panel';
import {Dashboard} from './dashboard/dashboard';
import {Warranty} from './warranty/warranty';
import {Service} from './service/service';
import {Repair} from './repair/repair';
import {Invoice} from './invoice/invoice';
import {SharedModule} from '../../shared/shared-module';
import {UserCommentComponent} from './comment/user-comment.component';
import {UserCommentListComponent} from './comment/user-comment-list/user-comment-list.component';
import {UserTicketComponent} from './ticket/user-ticket.component';
import {UserTicketListComponent} from './ticket/user-ticket-list/user-ticket-list.component';
import {PanelsModule} from '../../shared/panels-component/panels-module';
import {UserOrderComponent} from './order/user-order.component';
import {UserOrderListComponent} from './order/user-order-list/user-order-list.component';
import {UserPaymentComponent} from './payment/user-payment.component';
import {UserPaymentListComponent} from './payment/user-payment-list/user-payment-list.component';
import {UserOrderDetailsComponent} from './order/user-order-details/user-order-details.component';
import {UserPeriodicServiceComponent} from './periodic-service/user-periodic-service.component';
import {UserPeriodicServiceListComponent} from './periodic-service/user-periodic-service-list/user-periodic-service-list.component';

@NgModule({
  declarations: [
    UserPanel,
    Dashboard,
    Warranty,
    Service,
    Repair,
    Invoice,

    UserCommentComponent,
    UserCommentListComponent,

    UserTicketComponent,
    UserTicketListComponent,

    UserOrderComponent,
    UserOrderListComponent,
    UserOrderDetailsComponent,

    UserPaymentComponent,
    UserPaymentListComponent,

    UserPeriodicServiceComponent,
    UserPeriodicServiceListComponent,
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
