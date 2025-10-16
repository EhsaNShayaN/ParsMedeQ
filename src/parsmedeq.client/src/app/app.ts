import {Component, OnInit, Inject, Renderer2} from '@angular/core';
import {AppSettings, Settings} from './app.settings';
import {CartService} from './core/services/cart.service';
import {AuthService} from './core/services/auth.service';
import {OverlayContainer} from '@angular/cdk/overlay';
import {DOCUMENT} from '@angular/common';

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
              private authService: AuthService,
              private overlayContainer: OverlayContainer,
              private renderer: Renderer2,
              @Inject(DOCUMENT) private document: Document) {
    this.settings = this.appSettings.settings;
  }

  ngOnInit(): void {
    //if (this.authService.isLoggedIn()) {
    this.cartService.loadCart();
    //}

    // Ensure Angular Material overlays (dialogs, menus, etc.) inherit theme and direction
    const containerEl = this.overlayContainer.getContainerElement();
    // Add the same classes used on the root app container so themed styles apply
    this.renderer.addClass(containerEl, 'app');
    this.renderer.addClass(containerEl, this.settings.theme);
    this.renderer.addClass(containerEl, `toolbar-${this.settings.toolbar}`);

    // Set direction on both overlay container and body so [dir] and .app[dir] styles take effect
    const dir = this.settings.rtl ? 'rtl' : 'ltr';
    this.renderer.setAttribute(containerEl, 'dir', dir);
    this.renderer.setAttribute(this.document.body, 'dir', dir);
  }
}
