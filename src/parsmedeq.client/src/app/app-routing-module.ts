import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {Home} from './pages/home/home';
import {About} from './pages/about/about';
import {Contact} from './pages/contact/contact';
import {Faq} from './pages/faq/faq';
import {Login} from './pages/login/login';
import {Articles} from './pages/articles/articles';
import {ArticleDetail} from './pages/articles/article-detail/article-detail';
import {News} from './pages/news/news';
import {NewsDetail} from './pages/news/news-detail/news-detail';
import {Products} from './pages/products/products';
import {SignUp} from './pages/sign-up/sign-up';
import {ForgotPassword} from './pages/forgot-password/forgot-password';
import {AuthGuard} from './core/guards/auth.guard';
import {ProductDetail} from './pages/products/product-detail/product-detail';

const routes: Routes = [
  {path: '', component: Home},
  {path: 'about', component: About},
  {path: 'products', component: Products},
  {path: 'contact', component: Contact},
  {path: 'faq', component: Faq},
  {path: 'login', component: Login},
  {path: 'signup', component: SignUp},
  {path: 'forgot-password', component: ForgotPassword},
  {path: 'user-panel', loadChildren: () => import('./user-panel/user-panel-module').then(m => m.UserPanelModule), canActivate: [AuthGuard]},
  {path: 'products', component: Products},
  {path: 'products/:id', component: ProductDetail},
  {path: 'articles', component: Articles},
  {path: 'articles/:id', component: ArticleDetail},
  {path: 'news', component: News},
  {path: 'news/:id', component: NewsDetail},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
