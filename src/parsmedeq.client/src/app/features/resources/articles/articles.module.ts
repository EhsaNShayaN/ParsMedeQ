import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {SharedModule} from '../../../shared/shared.module';
import {ArticlesComponent} from './articles.component';
import {ArticleComponent} from './article/article.component';
import {AddCallArticleComponent} from './add-call-article/add-call-article.component';

export const routes: Routes = [
  {path: '', component: ArticlesComponent, pathMatch: 'full'},
  {path: 'add', component: AddCallArticleComponent},
  {path: ':id', component: ArticleComponent},
  {path: ':id/:title', component: ArticleComponent},
];

@NgModule({
  declarations: [
    ArticlesComponent,
    ArticleComponent,
    AddCallArticleComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ]
})
export class ArticlesModule {
}
