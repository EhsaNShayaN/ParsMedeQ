import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {Articles} from './articles';
import {ArticleList} from './article-list/article-list';
import {ArticleDetail} from './article-detail/article-detail';

const routes: Routes = [
  {path: '', component: Articles},
  {path: 'list', component: ArticleList},
  {path: ':id', component: ArticleDetail}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ArticlesRoutingModule { }
