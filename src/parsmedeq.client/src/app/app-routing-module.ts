import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {Home} from './pages/home/home';
import {About} from './pages/about/about';
import {Articles} from './pages/articles/articles';
import {Contact} from './pages/contact/contact';
import {Faq} from './pages/faq/faq';
import {Login} from './pages/login/login';
import {News} from './pages/news/news';
import {Products} from './pages/products/products';
import {SignUp} from './pages/sign-up/sign-up';
import {ForgotPassword} from './pages/forgot-password/forgot-password';

const routes: Routes = [
  {path: '', component: Home},
  {path: 'about', component: About},
  {path: 'products', component: Products},
  {path: 'articles', component: Articles},
  {path: 'news', component: News},
  {path: 'contact', component: Contact},
  {path: 'faq', component: Faq},
  {path: 'login', component: Login},
  {path: 'signup', component: SignUp},
  {path: 'forgot-password', component: ForgotPassword},
  {path: 'user-panel', loadChildren: () => import('./user-panel/user-panel-module').then(m => m.UserPanelModule)}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
