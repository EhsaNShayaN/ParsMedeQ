import {Injectable} from '@angular/core';
import {HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {Router} from '@angular/router';
import {AuthService} from './auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(
    private auth: AuthService,
    private router: Router) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.auth.getToken();

    let cloned = req;

    if (token) {
      cloned = cloned.clone({
        setHeaders: {Authorization: `Bearer ${token}`}
      });
    }

    const lang = sessionStorage.getItem('lang') ?? 'fa';
    cloned = cloned.clone({
      headers: cloned.headers.set('Accept-Language', lang)
    });

    return next.handle(cloned).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 406) {
          // ❗ Remove token (optional)
          //this.auth.logout();
          // ❗ Redirect to login page
          this.router.navigate(['/406']);
        }
        return throwError(() => error);
      })
    );
  }
}
