import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {Articles} from './articles';
import {ArticleDetail} from './article-detail/article-detail';

const routes: Routes = [
  {path: '', component: Articles},
  {path: ':id', component: ArticleDetail},
  {path: ':id/:title', component: ArticleDetail},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ArticlesRoutingModule {
}
