import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {JwtHelperService} from '@auth0/angular-jwt';
import {BehaviorSubject} from 'rxjs';
import {CustomConstants} from '../constants/custom.constants';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginSubject = new BehaviorSubject<boolean>(false);
  login$ = this.loginSubject.asObservable();

  constructor(private router: Router,
              public jwtHelper: JwtHelperService) {
  }

  // ورود کاربر و ذخیره JWT
  login(token: string): void {
    localStorage.setItem(CustomConstants.tokenName, token);
    this.loginSubject.next(true);
  }

  // دریافت توکن
  getToken(): string | null {
    return localStorage.getItem(CustomConstants.tokenName);
  }

  isAdmin(): boolean {
    return this.userInRole('superadmin') || this.userInRole('admin');
  }

  userInRole(role: string): boolean {
    if (!this.isLoggedIn()) {
      return false;
    }
    const decodedToken = this.jwtHelper.decodeToken(this.getToken() ?? '');
    const roles = decodedToken.role?.split(',');
    return !!roles.find((s: string) => s.toLowerCase() === role);
  }

  // بررسی ورود کاربر
  isLoggedIn(): boolean {
    const token = this.getToken();
    return !!token && !this.jwtHelper.isTokenExpired(token); // چک می‌کنه که توکن باشه و منقضی نشده باشه
  }

  // خروج کاربر
  logout(): void {
    localStorage.removeItem(CustomConstants.tokenName);
    this.router.navigate(['/']);
  }
}
