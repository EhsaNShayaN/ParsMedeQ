import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {TicketComponent} from './ticket.component';
import {SharedModule} from '../../shared/shared-module';

export const routes: Routes = [
  {path: '', component: TicketComponent, pathMatch: 'full'},
  {path: ':id', component: TicketComponent, pathMatch: 'full'}
];

@NgModule({
  declarations: [TicketComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ]
})
export class TicketModule {
}
