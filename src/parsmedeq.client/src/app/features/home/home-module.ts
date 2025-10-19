import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {Home} from './home';
import {SharedModule} from '../../shared/shared-module';
import {HomeRoutingModule} from './home-routing-module';
import {TranslateModule} from '@ngx-translate/core';
import {LoadingComponent} from "../../shared/loading.component/loading.component";

@NgModule({
  declarations: [
    Home
  ],
  imports: [
    CommonModule,
    SharedModule,
    HomeRoutingModule,
    TranslateModule,
    LoadingComponent,
  ]
})
export class HomeModule {
}
