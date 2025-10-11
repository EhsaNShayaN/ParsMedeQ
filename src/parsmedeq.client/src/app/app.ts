import {Component, OnInit} from '@angular/core';
import {AppSettings, Settings} from './app.settings';
import {CartService} from './core/services/cart.service';
import {AuthService} from './core/services/auth.service';

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
              private authService: AuthService) {
    this.settings = this.appSettings.settings;
  }

  ngOnInit(): void {
    if (this.authService.isLoggedIn()) {
      this.cartService.loadCart();
    }
  }
}
