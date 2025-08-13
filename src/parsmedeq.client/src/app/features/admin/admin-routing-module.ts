import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {Admin} from './admin';
import {ArticleComponent} from './resource/article/article.component';
import {ArticleListComponent} from './resource/article/article-list/article-list.component';
import {ArticleAddComponent} from './resource/article/article-add/article-add.component';
import {ArticleCategoryListComponent} from './resource/article/article-category-list/article-category-list.component';
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

const routes: Routes = [
  {path: '', component: Admin},

  {path: 'article', component: ArticleComponent},
  {path: 'article/list', component: ArticleListComponent},
  {path: 'article/add', component: ArticleAddComponent},
  {path: 'article/edit/:id', component: ArticleAddComponent},
  {path: 'article/category-list', component: ArticleCategoryListComponent},
  {path: 'article/add-category', component: ArticleCategoryAddComponent},
  {path: 'article/edit-category/:id', component: ArticleCategoryAddComponent},

  {path: 'notice', component: NoticeComponent},
  {path: 'notice/list', component: NoticeListComponent},
  {path: 'notice/add', component: NoticeAddComponent},
  {path: 'notice/edit/:id', component: NoticeAddComponent},
  {path: 'notice/category-list', component: NoticeCategoryListComponent},
  {path: 'notice/add-category', component: NoticeCategoryAddComponent},
  {path: 'notice/edit-category/:id', component: NoticeCategoryAddComponent},

  {path: 'clip', component: ClipComponent},
  {path: 'clip/list', component: ClipListComponent},
  {path: 'clip/add', component: ClipAddComponent},
  {path: 'clip/edit/:id', component: ClipAddComponent},
  {path: 'clip/category-list', component: ClipCategoryListComponent},
  {path: 'clip/add-category', component: ClipCategoryAddComponent},
  {path: 'clip/edit-category/:id', component: ClipCategoryAddComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule {
}
