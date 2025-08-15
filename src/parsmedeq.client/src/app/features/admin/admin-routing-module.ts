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
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule {
}
