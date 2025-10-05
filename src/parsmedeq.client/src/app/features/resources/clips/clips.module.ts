import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {ClipsComponent} from './clips.component';
import {ClipComponent} from './clip/clip.component';
import {SharedModule} from '../../../shared/shared-module';

export const routes: Routes = [
  {path: '', component: ClipsComponent, pathMatch: 'full'},
  {path: ':id', component: ClipComponent},
  {path: ':id/:title', component: ClipComponent},
];

@NgModule({
  declarations: [
    ClipsComponent,
    ClipComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ]
})
export class ClipsModule {
}
