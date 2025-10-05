import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {SharedModule} from '../../../shared/shared.module';
import {NoticesComponent} from './notices.component';
import {NoticeComponent} from './notice/notice.component';

export const routes: Routes = [
  {path: '', component: NoticesComponent, pathMatch: 'full'},
  {path: ':id', component: NoticeComponent},
  {path: ':id/:title', component: NoticeComponent},
];

@NgModule({
  declarations: [
    NoticesComponent,
    NoticeComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ]
})
export class NoticesModule {
}
