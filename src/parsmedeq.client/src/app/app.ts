import {Component, OnInit} from '@angular/core';
import {AppSettings, Settings} from './app.settings';
import {CartService} from './core/services/cart.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.scss',
  standalone: false,
})
export class App implements OnInit {
  public settings: Settings;

  constructor(public appSettings: AppSettings,
              private cartService: CartService) {
    this.settings = this.appSettings.settings;
  }

  ngOnInit(): void {
    const userId = localStorage.getItem('userId') ?? '';
    if (userId) {
      this.cartService.loadCart(userId);
    }
  }
}
