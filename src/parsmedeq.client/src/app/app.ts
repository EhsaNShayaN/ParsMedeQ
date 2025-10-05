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

      this.cartSignalR.onCartVisualUpdate((data) => {
        this.cartService.loadCart(userId);

        data.modifiedItems.forEach((item: any) => {
          if (item.delta < 0) {
            this.toastr.warning(
              `محصول ${item.ProductName} کاهش یافت. تعداد جدید: ${item.Quantity}`,
              'کاهش تعداد',
              {closeButton: true, progressBar: true, timeOut: 5000}
            );
          }
        });

        data.removedItems.forEach((item: any) => {
          this.toastr.error(
            `محصول ${item.ProductName} حذف شد`,
            'حذف محصول',
            {closeButton: true, progressBar: true, timeOut: 5000}
          );
        });

        if (data.modifiedItems.length === 0 && data.removedItems.length === 0) {
          this.toastr.success(data.message, 'سبد خرید', {timeOut: 3000});
        }
      });
    }
  }
}
