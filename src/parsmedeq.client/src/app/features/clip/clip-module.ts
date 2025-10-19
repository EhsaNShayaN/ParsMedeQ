import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SharedModule} from '../../shared/shared-module';
import {ClipListComponent} from './clip-list/clip-list.component';
import {ClipDetailComponent} from './clip-detail/clip-detail.component';
import {ClipRoutingModule} from './clip-routing-module';

@NgModule({
  declarations: [
    ClipListComponent,
    ClipDetailComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ClipRoutingModule,
  ]
})
export class ClipModule {
}
