import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {Home3} from './home3';

const routes: Routes = [{path: '', component: Home3}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class Home3RoutingModule {
}
