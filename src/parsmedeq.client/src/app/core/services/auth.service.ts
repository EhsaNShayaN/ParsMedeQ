import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BehaviorSubject, tap} from 'rxjs';

@Injectable({providedIn: 'root'})
export class AuthService {
  private baseUrl = 'https://api.example.com/auth'; // آدرس API خودتان
  private tokenKey = 'access_token';

  isLoggedIn = new BehaviorSubject<boolean>(this.hasToken());

  constructor(private http: HttpClient) {
  }

  login(credentials: { username: string; password: string }) {
    return this.http.post<{ token: string }>(`${this.baseUrl}/login`, credentials).pipe(
      tap(response => {
        localStorage.setItem(this.tokenKey, response.token);
        this.isLoggedIn.next(true);
      })
    );
  }

  signup(data: any) {
    return this.http.post(`${this.baseUrl}/signup`, data);
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.isLoggedIn.next(false);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  hasToken(): boolean {
    return !!this.getToken();
  }
}
