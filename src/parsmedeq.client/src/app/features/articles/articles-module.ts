import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ArticlesRoutingModule} from './articles-routing-module';
import {Articles} from './articles';
import {ArticleDetail} from './article-detail/article-detail';
import {SharedModule} from '../../shared/shared-module';


@NgModule({
  declarations: [
    Articles,
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
