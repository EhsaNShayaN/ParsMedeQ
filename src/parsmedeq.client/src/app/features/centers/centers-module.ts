import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SharedModule} from '../../shared/shared-module';
import {TranslateModule} from '@ngx-translate/core';
import {CentersComponent} from './centers.component';
import {CentersRoutingModule} from './centers-routing-module';

@NgModule({
  declarations: [
    CentersComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    CentersRoutingModule,
    TranslateModule,
  ]
})
export class CentersModule {
}
