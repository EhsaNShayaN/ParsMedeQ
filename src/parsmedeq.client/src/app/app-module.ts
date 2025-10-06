import {APP_INITIALIZER, NgModule, provideBrowserGlobalErrorListeners} from '@angular/core';
import {BrowserModule, DomSanitizer} from '@angular/platform-browser';
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule, provideHttpClient} from '@angular/common/http';
import {AuthInterceptor} from './core/services/auth.interceptor';
import {MatIconRegistry} from '@angular/material/icon';
import {AppRoutingModule} from './app-routing-module';
import {App} from './app';
import {Pages} from './features/pages';
import {SharedModule} from './shared/shared-module';
import {Header} from './shared/layouts/header/header';
import {Footer} from './shared/layouts/footer/footer';
import {UserLayout} from './shared/layouts/user-layout/user-layout';
import {AdminLayout} from './shared/layouts/admin-layout/admin-layout';
import {AdminSidebar} from './shared/components/admin-sidebar/admin-sidebar';
import {UserSidebar} from './shared/components/user-sidebar/user-sidebar';
import {AdminHeader} from './shared/components/admin-header/admin-header';
import {UserHeader} from './shared/components/user-header/user-header';
import {AppSettings} from './app.settings';
import {UrlSerializer} from '@angular/router';
import {LowerCaseUrlSerializer} from './core/pipes/lower-case-url-serializer.pipe';
import {UrlInitService} from './core/services/url-init.service';
import {Lang} from './theme/components/lang/lang';
import {MatMiniFabButton} from '@angular/material/button';
import {MatCardAvatar, MatCardHeader} from '@angular/material/card';
import {MatLine} from '@angular/material/core';
import {Toolbar} from './theme/components/toolbar/toolbar';
import {SocialIcons} from './theme/components/social-icons/social-icons';
import {UserMenu} from './theme/components/user-menu/user-menu';
import {JwtModule} from '@auth0/angular-jwt';
import {TranslateLoader, TranslateModule} from '@ngx-translate/core';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import {BidiModule} from '@angular/cdk/bidi';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {AuthGuard} from './core/guards/auth.guard';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

export function urlInitFactory(urlInitService: UrlInitService) {
  return () => urlInitService.init();
}

export function tokenGetter() {
  return localStorage.getItem('jwt');
}

@NgModule({
  declarations: [
    App,
    Pages,
    Toolbar,
    SocialIcons,
    UserMenu,
    ////////////////////////////
    Lang,
    Header,
    Footer,
    UserLayout,
    AdminLayout,
    AdminSidebar,
    UserSidebar,
    AdminHeader,
    UserHeader,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    BidiModule,
    AppRoutingModule,
    SharedModule,
    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: [],
      }
    }),
    MatMiniFabButton,
    MatCardAvatar,
    MatCardHeader,
    MatLine,
    TranslateModule.forRoot({
      defaultLanguage: 'fa',
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
    }),
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(),
    AppSettings,
    AuthGuard,
    {provide: APP_INITIALIZER, useFactory: urlInitFactory, deps: [UrlInitService], multi: true},
    {provide: UrlSerializer, useClass: LowerCaseUrlSerializer},
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
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
