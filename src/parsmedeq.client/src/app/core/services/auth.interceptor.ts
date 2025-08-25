import {Injectable, Injector} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {AuthService} from './auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private injector: Injector,
              private auth: AuthService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.auth.getToken();
    if (token) {
      const cloned = req.clone({
        setHeaders: {Authorization: `Bearer ${token}`}
      });
      return next.handle(cloned);
    }
    /*const languageService = this.injector.get(LanguageService); // Lazy resolve
    const lang = languageService.getLang();
    req = req.clone({
      headers: req.headers.set('Accept-Language', lang),
    });*/
    return next.handle(req);
  }
}
