import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserPanelRoutingModule } from './user-panel-routing-module';
import { UserPanel } from './user-panel';
import { Dashboard } from './dashboard/dashboard';
import { Orders } from './orders/orders';
import { Warranty } from './warranty/warranty';
import { ServiceRequest } from './service-request/service-request';
import { RepairRequest } from './repair-request/repair-request';
import { PreInvoice } from './pre-invoice/pre-invoice';
import { Comments } from './comments/comments';


@NgModule({
  declarations: [
    UserPanel,
    Dashboard,
    Orders,
    Warranty,
    ServiceRequest,
    RepairRequest,
    PreInvoice,
    Comments
  ],
  imports: [
    CommonModule,
    UserPanelRoutingModule
  ]
})
export class UserPanelModule { }
