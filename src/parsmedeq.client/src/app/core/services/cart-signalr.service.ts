import {Injectable, signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {StorageService} from './storage.service';
import {Cart, CartItem} from '../models/Cart';

@Injectable({providedIn: 'root'})
export class CartService {
  private apiUrl = '/api/cart';
  cart = signal<Cart | null>(null);

  constructor(private http: HttpClient, private storage: StorageService) {
  }

  /** گرفتن سبد */
  loadCart(userId?: string): void {
    const anonymousId = userId ? null : this.storage.getAnonymousId();
    this.http.get<Cart>(`${this.apiUrl}?userId=${userId ?? ''}&anonymousId=${anonymousId ?? ''}`)
      .subscribe(c => this.cart.set(c));
  }

  /** افزودن به سبد */
  addToCart(item: CartItem, userId?: string): void {
    const anonymousId = userId ? null : this.storage.getAnonymousId();
    this.http.post<Cart>(`${this.apiUrl}/add?userId=${userId ?? ''}&anonymousId=${anonymousId ?? ''}`, item)
      .subscribe(c => this.cart.set(c));
  }

  /** حذف از سبد */
  removeFromCart(itemId: string, userId?: string): void {
    const anonymousId = userId ? null : this.storage.getAnonymousId();
    this.http.delete<Cart>(`${this.apiUrl}/remove?userId=${userId ?? ''}&anonymousId=${anonymousId ?? ''}&itemId=${itemId}`)
      .subscribe(c => this.cart.set(c));
  }

  /** ادغام بعد از لاگین */
  mergeCart(userId: string): void {
    const anonymousId = this.storage.getAnonymousId();
    this.http.post<Cart>(`${this.apiUrl}/merge?anonymousId=${anonymousId}&userId=${userId}`, {})
      .subscribe(c => {
        this.cart.set(c);
        this.storage.clearAnonymousId(); // بعد از ادغام، ناشناس پاک میشه
      });
  }
}
