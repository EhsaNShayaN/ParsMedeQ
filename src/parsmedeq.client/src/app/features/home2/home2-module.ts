import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {Home2} from './home2';
import {SharedModule} from '../../shared/shared-module';
import {Home2RoutingModule} from './home2-routing-module';
import {TranslateModule} from '@ngx-translate/core';

@NgModule({
  declarations: [
    Home2
  ],
  imports: [
    CommonModule,
    SharedModule,
    Home2RoutingModule,
    TranslateModule,
  ]
})
export class Home2Module {
}
