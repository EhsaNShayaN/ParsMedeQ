import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private router: Router,
              public jwtHelper: JwtHelperService) {
  }

  // ورود کاربر و ذخیره JWT
  login(token: string): void {
    localStorage.setItem('token', token);
  }

  // دریافت توکن
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isAdmin(): boolean {
    return true;
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
    localStorage.removeItem('token');
    this.router.navigate(['/']);
  }
}
