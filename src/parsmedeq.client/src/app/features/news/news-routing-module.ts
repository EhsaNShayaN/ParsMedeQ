import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {News} from './news';
import {NewsList} from '../news/news-list/news-list';
import {NewsDetail} from '../news/news-detail/news-detail';

const routes: Routes = [
  {path: '', component: News},
  {path: 'list', component: NewsList},
  {path: ':id', component: NewsDetail}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NewsRoutingModule {
}
