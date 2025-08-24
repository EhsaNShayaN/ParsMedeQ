import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {Home} from './home';
import {SharedModule} from '../../shared/shared-module';
import {HomeRoutingModule} from './home-routing-module';
import {TranslateModule} from '@ngx-translate/core';

@NgModule({
  declarations: [
    Home
  ],
  imports: [
    CommonModule,
    SharedModule,
    HomeRoutingModule,
    TranslateModule,
  ]
})
export class HomeModule {
}
