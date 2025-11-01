import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AdminRoutingModule} from './admin-routing-module';
import {Admin} from './admin';
import {Dashboard} from './dashboard/dashboard';
import {Users} from './users/users';
import {SharedModule} from '../../shared/shared-module';
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
import {AngularEditorModule} from '@kolkov/angular-editor';
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from '@angular/material/datepicker';
import {NgxMatTimepickerComponent, NgxMatTimepickerDirective} from 'ngx-mat-timepicker';
import {MatSlideToggle} from '@angular/material/slide-toggle';
import {ProductComponent} from './product/product.component';
import {ProductListComponent} from './product/product-list/product-list.component';
import {ProductAddComponent} from './product/product-add/product-add.component';
import {ProductCategoryListComponent} from './product/product-category-list/product-category-list.component';
import {ProductCategoryAddComponent} from './product/product-category-add/product-category-add.component';
import {ProductMediaListComponent} from './product/product-media-list/product-media-list.component';
import {AdminTicketComponent} from './ticket/admin-ticket.component';
import {AdminTicketListComponent} from './ticket/admin-ticket-list/admin-ticket-list.component';
import {PanelsModule} from '../../shared/panels-component/panels-module';
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

@NgModule({
  declarations: [
    Admin,
    Dashboard,
    Users,

    ArticleComponent,
    ArticleListComponent,
    ArticleAddComponent,
    ArticleCategoryListComponent,
    ArticleCategoryAddComponent,

    NewsComponent,
    NewsListComponent,
    NewsAddComponent,
    NewsCategoryListComponent,
    NewsCategoryAddComponent,

    ClipComponent,
    ClipListComponent,
    ClipAddComponent,
    ClipCategoryListComponent,
    ClipCategoryAddComponent,

    ProductComponent,
    ProductListComponent,
    ProductAddComponent,
    ProductCategoryListComponent,
    ProductCategoryAddComponent,
    ProductMediaListComponent,

    AdminCommentComponent,
    AdminCommentListComponent,

    AdminTicketComponent,
    AdminTicketListComponent,

    AdminOrderComponent,
    AdminOrderDetailsComponent,
    AdminOrderListComponent,

    AdminPaymentComponent,
    AdminPaymentListComponent,

    TreatmentCenterComponent,
    TreatmentCenterListComponent,
    TreatmentCenterAddComponent,
    TreatmentCenterAddComponent,

    ServiceComponent,
    ServiceListComponent,
    ServiceAddComponent,
    ServiceAddComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    PanelsModule,
    AdminRoutingModule,
    AngularEditorModule,
    MatDatepickerToggle,
    MatDatepicker,
    MatDatepickerInput,
    NgxMatTimepickerDirective,
    NgxMatTimepickerComponent,
    MatSlideToggle,
  ]
})
export class AdminModule {
}
