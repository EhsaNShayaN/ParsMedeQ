import {Injectable, signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {StorageService} from './storage.service';
import {Cart, CartResponse} from '../models/Cart';
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
    const url = `${this.apiUrl}list?anonymousId=${anonymousId}`;
    this.http.get<CartResponse>(url).subscribe({
      next: (c) => {
        this.cart.set(c.data);
        this.cartSubject.next(c.data);
      },
      error: (err) => console.error('خطا در گرفتن سبد خرید', err)
    });
  }

  /** افزودن به سبد */
  addToCart(model: any): void {
    model.anonymousId = this.storage.getAnonymousId();
    this.http.post<CartResponse>(`${this.apiUrl}add`, model)
      .subscribe(c => {
        this.cart.set(c.data);
        this.cartSubject.next(c.data);
      });
  }

  /** حذف از سبد */
  removeFromCart(relatedId: number): void {
    console.log('relatedId', relatedId);
    const anonymousId = this.storage.getAnonymousId();
    const model: any = {relatedId, anonymousId};
    this.http.post<CartResponse>(`${this.apiUrl}remove`, model)
      .subscribe(c => {
        this.cart.set(c.data);
        this.cartSubject.next(c.data);
      });
  }

  /** ادغام بعد از لاگین */
  mergeCart(): void {
    const anonymousId = this.storage.getAnonymousId();
    const model: any = {anonymousId};
    this.http.post<CartResponse>(`${this.apiUrl}merge `, model)
      .subscribe(c => {
        this.cart.set(c.data);
        this.cartSubject.next(c.data);
        this.storage.clearAnonymousId(); // بعد از ادغام، ناشناس پاک میشه
      });
  }
}
