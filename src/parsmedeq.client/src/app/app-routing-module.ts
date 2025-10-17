import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {UserLayout} from './shared/layouts/user-layout/user-layout';
import {AdminLayout} from './shared/layouts/admin-layout/admin-layout';
import {Pages} from './features/pages';
import {AuthGuard} from './core/guards/auth.guard';

const pagesChildren: Routes = [
// صفحات عمومی
  {
    path: '',
    children: [
      {path: '', loadChildren: () => import('./features/home/home-module').then(m => m.HomeModule)},
      {path: 'home', loadChildren: () => import('./features/home/home-module').then(m => m.HomeModule)},
      {path: 'home2', loadChildren: () => import('./features/home2/home2-module').then(m => m.Home2Module)},
      {path: 'cart', loadChildren: () => import('./features/cart/cart-module').then(m => m.CartModule)},
      {path: 'about', loadChildren: () => import('./features/about/about-module').then(m => m.AboutModule)},
      {path: 'articles', loadChildren: () => import('./features/resources/articles/articles.module').then(m => m.ArticlesModule)},
      {path: 'news', loadChildren: () => import('./features/resources/news/news.module').then(m => m.NewsModule)},
      {path: 'clips', loadChildren: () => import('./features/resources/clips/clips.module').then(m => m.ClipsModule)},
      {path: 'products', loadChildren: () => import('./features/products/products-module').then(m => m.ProductsModule)},
      {path: 'centers', loadChildren: () => import('./features/centers/centers-module').then(m => m.CentersModule)},
      //{path: 'auth', loadChildren: () => import('./features/auth/auth-module').then(m => m.AuthModule)},
      {path: 'auth', loadChildren: () => import('./features/auth/login/login.module').then(m => m.LoginModule)},
      {path: 'contact', loadChildren: () => import('./features/contact/contact-module').then(m => m.ContactModule)},
      {path: 'faq', loadChildren: () => import('./features/faq/faq-module').then(m => m.FaqModule)},
    ]
  },
  // پنل کاربر
  {
    path: 'user',
    component: UserLayout,
    children: [
      {path: '', loadChildren: () => import('./features/user-panel/user-panel-module').then(m => m.UserPanelModule)}
    ],
    canActivate: [AuthGuard]
  },
  // پنل مدیریت
  {
    path: 'admin',
    component: AdminLayout,
    children: [
      {path: '', loadChildren: () => import('./features/admin/admin-module').then(m => m.AdminModule)}
    ],
    canActivate: [AuthGuard]
  },
  // مسیر پیش‌فرض
];
const faPagesChildren: Routes = pagesChildren;
const routes: Routes = [
  {
    path: '',
    component: Pages, children: faPagesChildren
  },
  {
    path: 'fa',
    component: Pages, children: faPagesChildren
  },
  {
    path: 'en',
    component: Pages, children: pagesChildren
  },
  {path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {scrollPositionRestoration: 'enabled'})],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
