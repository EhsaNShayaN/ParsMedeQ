import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {NewsComponent} from './news.component';
import {NewsDetailsComponent} from './news-details/news-details.component';
import {SharedModule} from '../../../shared/shared-module';

export const routes: Routes = [
  {path: '', component: NewsComponent, pathMatch: 'full'},
  {path: ':id', component: NewsDetailsComponent},
  {path: ':id/:title', component: NewsDetailsComponent},
];

@NgModule({
  declarations: [
    NewsComponent,
    NewsDetailsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ]
})
export class NewsModule {
}
