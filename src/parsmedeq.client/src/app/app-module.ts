import {NgModule, provideBrowserGlobalErrorListeners} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {AuthInterceptor} from './core/services/auth.interceptor';
import {MatIconRegistry} from '@angular/material/icon';
import {DomSanitizer} from '@angular/platform-browser';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatListModule} from '@angular/material/list';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {AppRoutingModule} from './app-routing-module';
import {App} from './app';
import {Header} from './layout/header/header';
import {Footer} from './layout/footer/footer';
import {Home} from './pages/home/home';
import {About} from './pages/about/about';
import {Products} from './pages/products/products';
import {Articles} from './pages/articles/articles';
import {News} from './pages/news/news';
import {Contact} from './pages/contact/contact';
import {Faq} from './pages/faq/faq';
import {Login} from './pages/login/login';
import {SignUp} from './pages/sign-up/sign-up';
import {ForgotPassword} from './pages/forgot-password/forgot-password';
import { ProductDetail } from './pages/products/product-detail/product-detail';
import { NewsDetail } from './pages/news/news-detail/news-detail';
import { ArticleDetail } from './pages/articles/article-detail/article-detail';
import { CommentForm } from './pages/comment-form/comment-form';


@NgModule({
  declarations: [
    App,
    Header,
    Footer,
    Home,
    About,
    Products,
    Articles,
    News,
    Contact,
    Faq,
    Login,
    SignUp,
    ForgotPassword,
    ProductDetail,
    NewsDetail,
    ArticleDetail,
    CommentForm,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatSidenavModule,
    MatListModule,
    MatCardModule,
    MatInputModule,
    MatFormFieldModule,
    AppRoutingModule,
    FormsModule,
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true}
  ],
  bootstrap: [App]
})
export class AppModule {
  constructor(private iconRegistry: MatIconRegistry, private sanitizer: DomSanitizer) {
    this.iconRegistry.addSvgIcon(
      'instagram',
      this.sanitizer.bypassSecurityTrustResourceUrl('assets/icons/instagram.svg')
    );
    this.iconRegistry.addSvgIcon(
      'linkedin',
      this.sanitizer.bypassSecurityTrustResourceUrl('assets/icons/linkedin.svg')
    );
  }
}
