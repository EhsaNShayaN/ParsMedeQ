import {Injectable, signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {StorageService} from './storage.service';
import {Cart, CartItem} from '../models/Cart';
import {BehaviorSubject} from 'rxjs';

@Injectable({providedIn: 'root'})
export class CartService {
  private cartSubject = new BehaviorSubject<Cart | null>(null);
  cart$ = this.cartSubject.asObservable();
  private apiUrl = '/api/cart';
  cart = signal<Cart | null>(null);

  constructor(private http: HttpClient, private storage: StorageService) {
  }

  /** گرفتن سبد */
  loadCart(userId?: string) {
    const url = userId ? `${this.apiUrl}?userId=${userId}` : this.apiUrl;
    this.http.get<Cart>(url).subscribe({
      next: (c) => {
        this.cart.set(c);
        this.cartSubject.next(c);
      },
      error: (err) => console.error('خطا در گرفتن سبد خرید', err)
    });
  }

  /** افزودن به سبد */
  addToCart(item: CartItem, userId?: string): void {
    const anonymousId = userId ? null : this.storage.getAnonymousId();
    this.http.post<Cart>(`${this.apiUrl}/add?userId=${userId ?? ''}&anonymousId=${anonymousId ?? ''}`, item)
      .subscribe(c => {
        this.cart.set(c);
        this.cartSubject.next(c);
      });
  }

  /** حذف از سبد */
  removeFromCart(itemId: string, userId?: string): void {
    const anonymousId = userId ? null : this.storage.getAnonymousId();
    this.http.delete<Cart>(`${this.apiUrl}/remove?userId=${userId ?? ''}&anonymousId=${anonymousId ?? ''}&itemId=${itemId}`)
      .subscribe(c => {
        this.cart.set(c);
        this.cartSubject.next(c);
      });
  }

  /** ادغام بعد از لاگین */
  mergeCart(userId: string): void {
    const anonymousId = this.storage.getAnonymousId();
    this.http.post<Cart>(`${this.apiUrl}/merge?anonymousId=${anonymousId}&userId=${userId}`, {})
      .subscribe(c => {
        this.cart.set(c);
        this.cartSubject.next(c);
        this.storage.clearAnonymousId(); // بعد از ادغام، ناشناس پاک میشه
      });
  }

  clearCart(userId?: string) {
    const url = `https://localhost:5001/api/cart/clear`;
    this.http.post<Cart>(url, {userId}).subscribe(c => {
      this.cart.set(c);
      this.cartSubject.next(c);
    });
  }
}
