import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ArticlesRoutingModule} from './articles-routing-module';
import {Articles} from './articles';
import {ArticleList} from './article-list/article-list';
import {ArticleDetail} from './article-detail/article-detail';
import {SharedModule} from '../../shared/shared-module';


@NgModule({
  declarations: [
    Articles,
    ArticleList,
    ArticleDetail
  ],
  imports: [
    CommonModule,
    SharedModule,
    ArticlesRoutingModule,
  ]
})
export class ArticlesModule {
}
