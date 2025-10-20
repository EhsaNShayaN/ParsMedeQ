import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {ArticleListComponent} from './article-list/article-list.component';
import {ArticleDetailComponent} from './article-detail/article-detail.component';

const routes: Routes = [
  {path: '', component: ArticleListComponent},
  {path: ':id', component: ArticleDetailComponent},
  {path: ':id/:title', component: ArticleDetailComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ArticleRoutingModule {
}
