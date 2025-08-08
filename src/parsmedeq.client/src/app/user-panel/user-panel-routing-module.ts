import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {Dashboard} from './dashboard/dashboard';
import {Orders} from './orders/orders';
import {PreInvoice} from './pre-invoice/pre-invoice';
import {RepairRequest} from './repair-request/repair-request';
import {ServiceRequest} from './service-request/service-request';
import {Warranty} from './warranty/warranty';

const routes: Routes = [
  {path: '', component: Dashboard},
  {path: 'orders', component: Orders},
  {path: 'warranty', component: Warranty},
  {path: 'service-request', component: ServiceRequest},
  {path: 'repair-request', component: RepairRequest},
  {path: 'pre-invoice', component: PreInvoice},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserPanelRoutingModule {
}
