import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ForStudyRoutingModule} from './for-study-routing-module';
import {ForStudy} from './for-study';
import {SharedModule} from '../../shared/shared-module';


@NgModule({
  declarations: [
    ForStudy
  ],
  imports: [
    CommonModule,
    SharedModule,
    ForStudyRoutingModule,
  ]
})
export class ForStudyModule {
}
