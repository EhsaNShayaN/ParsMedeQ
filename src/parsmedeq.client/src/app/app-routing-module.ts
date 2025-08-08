import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {MainLayout} from './layouts/main-layout/main-layout';
import {UserLayout} from './layouts/user-layout/user-layout';
import {AdminLayout} from './layouts/admin-layout/admin-layout';

const routes: Routes = [
  // صفحات عمومی
  {
    path: '',
    component: MainLayout,
    children: [
      {path: '', loadChildren: () => import('./features/home/home-module').then(m => m.HomeModule)},
      {path: 'about', loadChildren: () => import('./features/about/about-module').then(m => m.AboutModule)},
      {path: 'articles', loadChildren: () => import('./features/articles/articles-module').then(m => m.ArticlesModule)},
      {path: 'auth', loadChildren: () => import('./features/auth/auth-module').then(m => m.AuthModule)},
      {path: 'contact', loadChildren: () => import('./features/contact/contact-module').then(m => m.ContactModule)},
      {path: 'faq', loadChildren: () => import('./features/faq/faq-module').then(m => m.FaqModule)},
      {path: 'news', loadChildren: () => import('./features/news/news-module').then(m => m.NewsModule)},
      {path: 'products', loadChildren: () => import('./features/products/products-module').then(m => m.ProductsModule)},
    ]
  },
  // پنل کاربر
  {
    path: 'user',
    component: UserLayout,
    children: [
      {path: '', loadChildren: () => import('./features/user-panel/user-panel-module').then(m => m.UserPanelModule)}
    ]
  },
  // پنل مدیریت
  {
    path: 'admin',
    component: AdminLayout,
    children: [
      {path: '', loadChildren: () => import('./features/admin/admin-module').then(m => m.AdminModule)}
    ]
  },
  // مسیر پیش‌فرض
  {path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {scrollPositionRestoration: 'enabled'})],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
