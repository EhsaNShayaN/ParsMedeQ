import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SharedModule} from '../../shared/shared-module';
import {NewsListComponent} from './news-list.component';
import {NewsDetailComponent} from './news-detail/news-detail.component';
import {NewsRoutingModule} from './news-routing-module';

@NgModule({
  declarations: [
    NewsListComponent,
    NewsDetailComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    NewsRoutingModule,
  ]
})
export class NewsModule {
}
