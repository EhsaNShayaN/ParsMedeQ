import {NgModule} from '@angular/core';
import {PanelCommentComponent} from './comment/panel-comment.component';
import {PanelCommentListComponent} from './comment/panel-comment-list/panel-comment-list.component';
import {PanelTicketComponent} from './ticket/panel-ticket.component';
import {PanelTicketListComponent} from './ticket/panel-ticket-list/panel-ticket-list.component';
import {SharedModule} from '../shared-module';
import {CommonModule} from '@angular/common';

@NgModule({
  declarations: [
    PanelCommentComponent,
    PanelCommentListComponent,

    PanelTicketComponent,
    PanelTicketListComponent,
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
  ],
  providers: [],
})
export class PanelsModule {
}
