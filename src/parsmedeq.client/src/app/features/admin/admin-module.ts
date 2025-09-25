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
import {MatCell, MatCellDef, MatColumnDef, MatHeaderCell, MatHeaderCellDef, MatHeaderRow, MatHeaderRowDef, MatRow, MatRowDef, MatTable} from '@angular/material/table';
import {MatSort} from '@angular/material/sort';
import {MatPaginator} from '@angular/material/paginator';
import {MatOption, MatSelect} from '@angular/material/select';
import {MatSlideToggle} from '@angular/material/slide-toggle';
import {ProductComponent} from './product/product.component';
import {ProductListComponent} from './product/product-list/product-list.component';
import { ProductAddComponent } from './product/product-add/product-add.component';
import {ProductCategoryListComponent} from './product/product-category-list/product-category-list.component';
import {ProductCategoryAddComponent} from './product/product-category-add/product-category-add.component';

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
  ],
  imports: [
    CommonModule,
    SharedModule,
    AdminRoutingModule,
    MatTable,
    MatSort,
    MatColumnDef,
    MatPaginator,
    MatHeaderCell,
    MatHeaderRow,
    MatRow,
    MatCell,
    MatSelect,
    MatOption,
    AngularEditorModule,
    MatDatepickerToggle,
    MatDatepicker,
    MatDatepickerInput,
    NgxMatTimepickerDirective,
    NgxMatTimepickerComponent,
    MatSlideToggle,
    MatHeaderCellDef,
    MatCellDef,
    MatHeaderRowDef,
    MatRowDef,
  ]
})
export class AdminModule {
}
