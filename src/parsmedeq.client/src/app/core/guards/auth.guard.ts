import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {AuthService} from '../services/auth.service';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService,
              private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let returnUrl = state.url;
    let flag = true;
    for (const url of route.url) {
      //returnUrl += '/' + url.path;
      const result = this.checkAdminUrl(url.path);
      if (!result) {
        flag = false;
        break;
      }
    }
    if (flag && this.authService.isLoggedIn()) {
      return true;
    }
    this.router.navigate(['/auth/login', returnUrl]);
    return false;
    /*if (this.authService.isLoggedIn()) {
      return true;
    } else {
      this.router.navigate(['/auth/login']);
      return false;
    }*/
  }

  checkAdminUrl(url: string): boolean {
    if (url.toLowerCase().includes('admin')) {
      if (!this.authService.isAdmin()) {
        this.router.navigate(['']);
        return false;
      }
    }
    return true;
  }
}
