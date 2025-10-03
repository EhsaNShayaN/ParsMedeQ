import {Injectable, signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Cart, CartItem} from '../models/Cart';

@Injectable({providedIn: 'root'})
export class CartService {
  private apiUrl = '/api/cart';
  cart = signal<Cart | null>(null);

  constructor(private http: HttpClient) {
  }

  getCart(userId?: string): void {
    this.http.get<Cart>(`${this.apiUrl}/${userId ?? ''}`).subscribe(c => this.cart.set(c));
  }

  addToCart(userId: string | null, item: CartItem): void {
    this.http.post<Cart>(`${this.apiUrl}/${userId ?? ''}`, item).subscribe(c => this.cart.set(c));
  }

  removeFromCart(userId: string | null, itemId: string): void {
    this.http.delete<Cart>(`${this.apiUrl}/${userId}/${itemId}`).subscribe(c => this.cart.set(c));
  }

  mergeCart(anonymousId: string, userId: string): void {
    this.http.post<Cart>(`${this.apiUrl}/merge?anonymousId=${anonymousId}&userId=${userId}`, {})
      .subscribe(c => this.cart.set(c));
  }

  checkPrice(cart: Cart): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}/check-price`, cart);
  }
}
