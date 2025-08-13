import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AdminRoutingModule} from './admin-routing-module';
import {Admin} from './admin';
import {Dashboard} from './dashboard/dashboard';
import {Users} from './users/users';
import {Products} from './products/products';
import {Articles} from './articles/articles';
import {SharedModule} from '../../shared/shared-module';
import {ArticleComponent} from './resource/article/article.component';
import {ArticleListComponent} from './resource/article/article-list/article-list.component';
import { ArticleAddComponent } from './resource/article/article-add/article-add.component';
import { ArticleCategoryListComponent } from './resource/article/article-category-list/article-category-list.component';
import {ArticleCategoryAddComponent} from './resource/article/article-category-add/article-category-add.component';
import {NoticeComponent} from './resource/notice/notice.component';
import {NoticeListComponent} from './resource/notice/notice-list/notice-list.component';
import {NoticeAddComponent} from './resource/notice/notice-add/notice-add.component';
import {NoticeCategoryListComponent} from './resource/notice/notice-category-list/notice-category-list.component';
import {NoticeCategoryAddComponent} from './resource/notice/notice-category-add/notice-category-add.component';
import {ClipComponent} from './resource/clip/clip.component';
import {ClipListComponent} from './resource/clip/clip-list/clip-list.component';
import {ClipAddComponent} from './resource/clip/clip-add/clip-add.component';
import {ClipCategoryListComponent} from './resource/clip/clip-category-list/clip-category-list.component';
import {ClipCategoryAddComponent} from './resource/clip/clip-category-add/clip-category-add.component';
import {AngularEditorModule} from '@kolkov/angular-editor';
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from '@angular/material/datepicker';
import {NgxMatTimepickerComponent, NgxMatTimepickerDirective} from 'ngx-mat-timepicker';
import {MatCell, MatColumnDef, MatHeaderCell, MatHeaderRow, MatRow, MatTable} from '@angular/material/table';
import {MatSort} from '@angular/material/sort';
import {MatPaginator} from '@angular/material/paginator';
import {MatOption, MatSelect} from '@angular/material/select';
import {MatError} from '@angular/material/form-field';
import {MatSlideToggle} from '@angular/material/slide-toggle';

@NgModule({
  declarations: [
    Admin,
    Dashboard,
    Users,
    Products,
    Articles,

    ArticleComponent,
    ArticleListComponent,
    ArticleAddComponent,
    ArticleCategoryListComponent,
    ArticleCategoryAddComponent,

    NoticeComponent,
    NoticeListComponent,
    NoticeAddComponent,
    NoticeCategoryListComponent,
    NoticeCategoryAddComponent,

    ClipComponent,
    ClipListComponent,
    ClipAddComponent,
    ClipCategoryListComponent,
    ClipCategoryAddComponent,
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
    MatError,
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
