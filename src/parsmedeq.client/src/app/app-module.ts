import {NgModule, provideBrowserGlobalErrorListeners} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {AuthInterceptor} from './core/services/auth.interceptor';
import {MatIconRegistry} from '@angular/material/icon';
import {DomSanitizer} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing-module';
import {App} from './app';
import {SharedModule} from './shared/shared-module';
import {Header} from './shared/layouts/header/header';
import {Footer} from './shared/layouts/footer/footer';
import {MainLayout} from './shared/layouts/main-layout/main-layout';
import {UserLayout} from './shared/layouts/user-layout/user-layout';
import {AdminLayout} from './shared/layouts/admin-layout/admin-layout';
import {AdminSidebar} from './shared/components/admin-sidebar/admin-sidebar';
import {UserSidebar} from './shared/components/user-sidebar/user-sidebar';
import {CommentForm} from './shared/components/comment-form/comment-form';
import {AdminHeader} from './shared/components/admin-header/admin-header';
import {UserHeader} from './shared/components/user-header/user-header';


@NgModule({
  declarations: [
    App,
    ////////////////////////////
    Header,
    Footer,
    MainLayout,
    UserLayout,
    AdminLayout,
    AdminSidebar,
    UserSidebar,
    AdminHeader,
    UserHeader,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    SharedModule,
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true}
  ],
  exports: [],
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
