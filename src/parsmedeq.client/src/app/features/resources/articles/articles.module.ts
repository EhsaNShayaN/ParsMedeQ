import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {ArticlesComponent} from './articles.component';
import {ArticleComponent} from './article/article.component';
import {SharedModule} from '../../../shared/shared-module';

export const routes: Routes = [
  {path: '', component: ArticlesComponent, pathMatch: 'full'},
  {path: ':id', component: ArticleComponent},
  {path: ':id/:title', component: ArticleComponent},
];

@NgModule({
  declarations: [
    ArticlesComponent,
    ArticleComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ]
})
export class ArticlesModule {
}
