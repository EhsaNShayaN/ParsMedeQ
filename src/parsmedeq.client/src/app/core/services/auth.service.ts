// src/app/services/auth.service.ts
import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {Observable} from 'rxjs';
import {tap} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:5001/api/auth'; // آدرس بک‌اند

  constructor(private http: HttpClient, private router: Router) {
  }

  // ورود کاربر و ذخیره JWT
  login(credentials: { email: string; password: string }): Observable<any> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, credentials)
      .pipe(
        tap((res) => {
          if (res.token) {
            localStorage.setItem('token', res.token);
          }
        })
      );
  }

  // دریافت توکن
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // بررسی ورود کاربر
  isLoggedIn(): boolean {
    const token = this.getToken();
    return !!token; // فقط چک می‌کنه که توکن باشه
  }

  // خروج کاربر
  logout(): void {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}
