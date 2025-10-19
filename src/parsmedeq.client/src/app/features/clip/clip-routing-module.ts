import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {ClipListComponent} from './clip-list/clip-list.component';
import {ClipDetailComponent} from './clip-detail/clip-detail.component';

const routes: Routes = [
  {path: '', component: ClipListComponent},
  {path: ':id', component: ClipDetailComponent},
  {path: ':id/:title', component: ClipDetailComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClipRoutingModule {
}
