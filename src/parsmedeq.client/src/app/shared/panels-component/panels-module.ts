import {NgModule} from '@angular/core';
import {PanelCommentComponent} from './comment/panel-comment.component';
import {PanelCommentListComponent} from './comment/panel-comment-list/panel-comment-list.component';
import {PanelTicketComponent} from './ticket/panel-ticket.component';
import {PanelTicketListComponent} from './ticket/panel-ticket-list/panel-ticket-list.component';
import {SharedModule} from '../shared-module';
import {CommonModule} from '@angular/common';
import {PanelOrderComponent} from './order/panel-order.component';
import {PanelOrderListComponent} from './order/panel-order-list/panel-order-list.component';
import {PanelPaymentComponent} from './payment/panel-payment.component';
import {PanelPaymentListComponent} from './payment/panel-payment-list/panel-payment-list.component';
import {PanelOrderDetailsComponent} from './order/panel-order-details/panel-order-details.component';

@NgModule({
  declarations: [
    PanelCommentComponent,
    PanelCommentListComponent,

    PanelTicketComponent,
    PanelTicketListComponent,

    PanelOrderComponent,
    PanelOrderDetailsComponent,
    PanelOrderListComponent,

    PanelPaymentComponent,
    PanelPaymentListComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
  ],
  exports: [
    PanelCommentComponent,
    PanelCommentListComponent,

    PanelTicketComponent,
    PanelTicketListComponent,

    PanelOrderComponent,
    PanelOrderDetailsComponent,
    PanelOrderListComponent,

    PanelPaymentComponent,
    PanelPaymentListComponent,
  ],
  providers: [],
})
export class PanelsModule {
}
