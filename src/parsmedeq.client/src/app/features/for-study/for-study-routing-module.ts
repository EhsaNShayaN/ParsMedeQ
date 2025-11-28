import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {ForStudy} from './for-study';

const routes: Routes = [{path: '', component: ForStudy}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ForStudyRoutingModule {
}
