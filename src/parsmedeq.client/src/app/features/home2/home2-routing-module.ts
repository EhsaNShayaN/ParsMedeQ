import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {Home2} from './home2';

const routes: Routes = [{path: '', component: Home2}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class Home2RoutingModule {
}
