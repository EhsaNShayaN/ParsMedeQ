import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {Home3} from './home3';
import {SharedModule} from '../../shared/shared-module';
import {Home3RoutingModule} from './home3-routing-module';
import {TranslateModule} from '@ngx-translate/core';

@NgModule({
  declarations: [
    Home3
  ],
  imports: [
    CommonModule,
    SharedModule,
    Home3RoutingModule,
    TranslateModule,
  ]
})
export class Home3Module {
}
