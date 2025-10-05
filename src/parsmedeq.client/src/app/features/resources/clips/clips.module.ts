import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {SharedModule} from '../../../shared/shared.module';
import {ClipsComponent} from './clips.component';
import {ClipComponent} from './clip/clip.component';

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
