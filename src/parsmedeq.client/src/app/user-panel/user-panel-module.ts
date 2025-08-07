import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {UserPanelRoutingModule} from './user-panel-routing-module';
import {UserPanel} from './user-panel';
import {Dashboard} from './dashboard/dashboard';
import {Orders} from './orders/orders';
import {Warranty} from './warranty/warranty';
import {ServiceRequest} from './service-request/service-request';
import {RepairRequest} from './repair-request/repair-request';
import {PreInvoice} from './pre-invoice/pre-invoice';
import {Comments} from './comments/comments';
import {MatList, MatListItem} from '@angular/material/list';
import {MatCell, MatCellDef, MatColumnDef, MatHeaderCell, MatHeaderCellDef, MatHeaderRow, MatHeaderRowDef, MatRow, MatRowDef, MatTable} from '@angular/material/table';
import {FormsModule} from '@angular/forms';
import {MatFormField, MatInput, MatLabel} from '@angular/material/input';
import {MatButton} from '@angular/material/button';


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
    UserPanelRoutingModule,
    MatList,
    MatListItem,
    MatTable,
    MatColumnDef,
    MatHeaderCell,
    MatCell,
    MatHeaderRow,
    MatRow,
    MatHeaderCellDef,
    MatCellDef,
    MatHeaderRowDef,
    MatRowDef,
    FormsModule,
    MatFormField,
    MatLabel,
    MatFormField,
    MatInput,
    MatLabel,
    MatFormField,
    MatButton
  ]
})
export class UserPanelModule {
}
