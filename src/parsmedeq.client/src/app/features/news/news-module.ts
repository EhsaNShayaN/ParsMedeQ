import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SharedModule} from '../../shared/shared-module';
import {NewsRoutingModule} from './news-routing-module';
import {News} from './news';
import {NewsList} from './news-list/news-list';
import {NewsDetail} from './news-detail/news-detail';

@NgModule({
  declarations: [
    News,
    NewsList,
    NewsDetail
  ],
  imports: [
    CommonModule,
    SharedModule,
    NewsRoutingModule,
  ]
})
export class NewsModule {
}
