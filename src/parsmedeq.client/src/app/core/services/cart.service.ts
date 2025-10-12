import {Injectable, signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {StorageService} from './storage.service';
import {Cart, CartItem} from '../models/Cart';
import {BehaviorSubject} from 'rxjs';
import {endpoint} from './cookie-utils';

@Injectable({providedIn: 'root'})
export class CartService {
  private cartSubject = new BehaviorSubject<Cart | null>(null);
  cart$ = this.cartSubject.asObservable();
  private apiUrl = `${endpoint()}cart/`;
  cart = signal<Cart | null>(null);

  constructor(private http: HttpClient,
              private storage: StorageService) {
  }

  /** گرفتن سبد */
  loadCart() {
    const anonymousId = this.storage.getAnonymousId();
    const url = `${this.apiUrl}list?anonymousId=${anonymousId ?? ''}`;
    this.http.get<Cart>(url).subscribe({
      next: (c) => {
        this.cart.set(c);
        this.cartSubject.next(c);
      },
      error: (err) => console.error('خطا در گرفتن سبد خرید', err)
    });
  }

  /** افزودن به سبد */
  addToCart(model: CartItem): void {
    const anonymousId = this.storage.getAnonymousId();
    this.http.post<Cart>(`${this.apiUrl}add?anonymousId=${anonymousId ?? ''}`, model)
      .subscribe(c => {
        this.cart.set(c);
        this.cartSubject.next(c);
      });
  }

  /** حذف از سبد */
  removeFromCart(relatedId: number): void {
    const anonymousId = this.storage.getAnonymousId();
    const model: any = {relatedId};
    this.http.post<Cart>(`${this.apiUrl}remove?anonymousId=${anonymousId ?? ''}`, model)
      .subscribe(c => {
        this.cart.set(c);
        this.cartSubject.next(c);
      });
  }

  /** ادغام بعد از لاگین */
  mergeCart(): void {
    const anonymousId = this.storage.getAnonymousId();
    this.http.post<Cart>(`${this.apiUrl}merge?anonymousId=${anonymousId}`, null)
      .subscribe(c => {
        this.cart.set(c);
        this.cartSubject.next(c);
        this.storage.clearAnonymousId(); // بعد از ادغام، ناشناس پاک میشه
      });
  }
}
