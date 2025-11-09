import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {Admin} from './admin';
import {ArticleComponent} from './resource/article/article.component';
import {ArticleListComponent} from './resource/article/article-list/article-list.component';
import {ArticleAddComponent} from './resource/article/article-add/article-add.component';
import {ArticleCategoryListComponent} from './resource/article/article-category-list/article-category-list.component';
import {ArticleCategoryAddComponent} from './resource/article/article-category-add/article-category-add.component';
import {NewsComponent} from './resource/news/news.component';
import {NewsListComponent} from './resource/news/news-list/news-list.component';
import {NewsAddComponent} from './resource/news/news-add/news-add.component';
import {NewsCategoryListComponent} from './resource/news/news-category-list/news-category-list.component';
import {NewsCategoryAddComponent} from './resource/news/news-category-add/news-category-add.component';
import {ClipComponent} from './resource/clip/clip.component';
import {ClipListComponent} from './resource/clip/clip-list/clip-list.component';
import {ClipAddComponent} from './resource/clip/clip-add/clip-add.component';
import {ClipCategoryListComponent} from './resource/clip/clip-category-list/clip-category-list.component';
import {ClipCategoryAddComponent} from './resource/clip/clip-category-add/clip-category-add.component';
import {ProductComponent} from './product/product.component';
import {ProductListComponent} from './product/product-list/product-list.component';
import {ProductAddComponent} from './product/product-add/product-add.component';
import {ProductCategoryListComponent} from './product/product-category-list/product-category-list.component';
import {ProductCategoryAddComponent} from './product/product-category-add/product-category-add.component';
import {ProductMediaListComponent} from './product/product-media-list/product-media-list.component';
import {AdminTicketComponent} from './ticket/admin-ticket.component';
import {AdminTicketListComponent} from './ticket/admin-ticket-list/admin-ticket-list.component';
import {AdminCommentComponent} from './comment/admin-comment.component';
import {AdminCommentListComponent} from './comment/admin-comment-list/admin-comment-list.component';
import {AdminOrderComponent} from './order/admin-order.component';
import {AdminOrderListComponent} from './order/admin-order-list/admin-order-list.component';
import {AdminPaymentComponent} from './payment/admin-payment.component';
import {AdminPaymentListComponent} from './payment/admin-payment-list/admin-payment-list.component';
import {AdminOrderDetailsComponent} from './orders/admin-order-details/admin-order-details.component';
import {TreatmentCenterListComponent} from './treatment-center/treatment-center-list/treatment-center-list.component';
import {TreatmentCenterAddComponent} from './treatment-center/treatment-center-add/treatment-center-add.component';
import {TreatmentCenterComponent} from './treatment-center/treatment-center.component';
import {ServiceComponent} from './service/service.component';
import {ServiceListComponent} from './service/service-list/service-list.component';
import {ServiceAddComponent} from './service/service-add/service-add.component';
import {AdminPeriodicServiceComponent} from './periodic-service/admin-periodic-service.component';
import {AdminPeriodicServiceListComponent} from './periodic-service/admin-periodic-service-list/admin-periodic-service-list.component';

const routes: Routes = [
  {path: '', component: Admin},

  {path: 'article', component: ArticleComponent},
  {path: 'article/list', component: ArticleListComponent},
  {path: 'article/add', component: ArticleAddComponent},
  {path: 'article/edit/:id', component: ArticleAddComponent},
  {path: 'article/category/list', component: ArticleCategoryListComponent},
  {path: 'article/category/add', component: ArticleCategoryAddComponent},
  {path: 'article/category/edit/:id', component: ArticleCategoryAddComponent},

  {path: 'news', component: NewsComponent},
  {path: 'news/list', component: NewsListComponent},
  {path: 'news/add', component: NewsAddComponent},
  {path: 'news/edit/:id', component: NewsAddComponent},
  {path: 'news/category/list', component: NewsCategoryListComponent},
  {path: 'news/category/add', component: NewsCategoryAddComponent},
  {path: 'news/category/edit/:id', component: NewsCategoryAddComponent},

  {path: 'clip', component: ClipComponent},
  {path: 'clip/list', component: ClipListComponent},
  {path: 'clip/add', component: ClipAddComponent},
  {path: 'clip/edit/:id', component: ClipAddComponent},
  {path: 'clip/category/list', component: ClipCategoryListComponent},
  {path: 'clip/category/add', component: ClipCategoryAddComponent},
  {path: 'clip/category/edit/:id', component: ClipCategoryAddComponent},

  {path: 'product', component: ProductComponent},
  {path: 'product/list', component: ProductListComponent},
  {path: 'product/add', component: ProductAddComponent},
  {path: 'product/edit/:id', component: ProductAddComponent},
  {path: 'product/category/list', component: ProductCategoryListComponent},
  {path: 'product/category/add', component: ProductCategoryAddComponent},
  {path: 'product/category/edit/:id', component: ProductCategoryAddComponent},
  {path: 'product/media/list/:id', component: ProductMediaListComponent},

  {path: 'comment', component: AdminCommentComponent},
  {path: 'comment/list', component: AdminCommentListComponent},

  {path: 'ticket', component: AdminTicketComponent},
  {path: 'ticket/list', component: AdminTicketListComponent},

  {path: 'order', component: AdminOrderComponent},
  {path: 'order/list', component: AdminOrderListComponent},
  {path: 'order/details/:id', component: AdminOrderDetailsComponent},

  {path: 'payment', component: AdminPaymentComponent},
  {path: 'payment/list', component: AdminPaymentListComponent},

  {path: 'treatment-center', component: TreatmentCenterComponent},
  {path: 'treatment-center/list', component: TreatmentCenterListComponent},
  {path: 'treatment-center/add', component: TreatmentCenterAddComponent},
  {path: 'treatment-center/edit/:id', component: TreatmentCenterAddComponent},

  {path: 'service', component: ServiceComponent},
  {path: 'service/list', component: ServiceListComponent},
  {path: 'service/add', component: ServiceAddComponent},
  {path: 'service/edit/:id', component: ServiceAddComponent},

  {path: 'periodic-service', component: AdminPeriodicServiceComponent},
  {path: 'periodic-service/list', component: AdminPeriodicServiceListComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule {
}
