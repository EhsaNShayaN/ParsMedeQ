import {APP_INITIALIZER, NgModule, provideBrowserGlobalErrorListeners} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {HTTP_INTERCEPTORS, HttpClientModule, provideHttpClient} from '@angular/common/http';
import {AuthInterceptor} from './core/services/auth.interceptor';
import {MatIconRegistry} from '@angular/material/icon';
import {DomSanitizer} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing-module';
import {App} from './app';
import {Pages} from './features/pages';
import {SharedModule} from './shared/shared-module';
import {Header} from './shared/layouts/header/header';
import {Footer} from './shared/layouts/footer/footer';
import {MainLayout} from './shared/layouts/main-layout/main-layout';
import {UserLayout} from './shared/layouts/user-layout/user-layout';
import {AdminLayout} from './shared/layouts/admin-layout/admin-layout';
import {AdminSidebar} from './shared/components/admin-sidebar/admin-sidebar';
import {UserSidebar} from './shared/components/user-sidebar/user-sidebar';
import {AdminHeader} from './shared/components/admin-header/admin-header';
import {UserHeader} from './shared/components/user-header/user-header';
import {AppSettings} from './app.settings';
import {UrlSerializer} from '@angular/router';
import {LowerCaseUrlSerializer} from './core/pipes/lower-case-url-serializer.pipe';
import {provideTranslateService} from '@ngx-translate/core';
import {provideTranslateHttpLoader} from '@ngx-translate/http-loader';
import {UrlInitService} from './core/services/url-init.service';
import {Lang} from './theme/components/lang/lang';


export function urlInitFactory(urlInitService: UrlInitService) {
  return () => urlInitService.init();
}

@NgModule({
  declarations: [
    App,
    Pages,
    ////////////////////////////
    Lang,
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
    provideHttpClient(),
    provideTranslateService({
      loader: provideTranslateHttpLoader({
        prefix: '/assets/i18n/',
        suffix: '.json'
      }),
      fallbackLang: 'fa',
      lang: 'fa'
    }),
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
    AppSettings,
    {provide: UrlSerializer, useClass: LowerCaseUrlSerializer},
    {
      provide: APP_INITIALIZER,
      useFactory: urlInitFactory,
      deps: [UrlInitService],
      multi: true,
    }
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
