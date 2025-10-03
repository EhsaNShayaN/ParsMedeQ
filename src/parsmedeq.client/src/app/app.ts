import {Component, OnInit} from '@angular/core';
import {AppSettings, Settings} from './app.settings';
import {CartSignalRService} from './core/services/cart-signalr.service';
import {CartService} from './core/services/cart.service';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.scss',
  standalone: false,
})
export class App implements OnInit {
  public settings: Settings;

  constructor(public appSettings: AppSettings,
              private cartService: CartService,
              private cartSignalR: CartSignalRService,
              private toastr: ToastrService) {
    this.settings = this.appSettings.settings;
  }

  ngOnInit(): void {
    const userId = localStorage.getItem('userId') ?? '';
    if (userId) {
      this.cartService.loadCart(userId);

      this.cartSignalR.startConnection(userId);

      this.cartSignalR.onCartUpdatedDetailed((data) => {
        const items = data.modifiedItems;
        if (items.length > 0) {
          items.forEach((item: any) => {
            const text = `محصول ${item.ProductName} تغییر کرد. تعداد جدید: ${item.Quantity}`;
            this.toastr.warning(text, 'سبد خرید', {
              progressBar: true,
              closeButton: true,
              timeOut: 6000,
              tapToDismiss: true,
            });
          });
        } else {
          this.toastr.warning(data.message, 'سبد خرید', {
            progressBar: true,
            closeButton: true,
            timeOut: 5000,
          });
        }

        // Auto-Refresh سبد
        this.cartService.loadCart(userId);
      });
    }
  }
}
