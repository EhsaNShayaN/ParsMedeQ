import {Component, Output, EventEmitter, ViewChild, OnDestroy, OnInit} from '@angular/core';
import {AppSettings, Settings} from '../../../app.settings';
import {UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {PureComponent} from '../../../pure-component';
import {MatMenuTrigger} from '@angular/material/menu';
import {AuthService} from '../../../core/services/auth.service';
import {CartItem} from '../../../core/models/Cart';
import {CartService} from '../../../core/services/cart.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.html',
  styleUrl: './toolbar.scss',
  standalone: false
})
export class Toolbar extends PureComponent implements OnInit, OnDestroy {
  cartItems: CartItem[] = [];
  totalItems = 0;
  @Output() onMenuIconClick: EventEmitter<any> = new EventEmitter<any>();
  public subscribeForm: UntypedFormGroup;
  public settings: Settings;
  public profile: any;
  @ViewChild(MatMenuTrigger, {static: false}) trigger!: MatMenuTrigger;
  recheckIfInMenu: boolean;
  subscribe1: any;

  constructor(private cartService: CartService,
              public appSettings: AppSettings,
              public formBuilder: UntypedFormBuilder,
              public authService: AuthService) {
    super();
    this.settings = this.appSettings.settings;
    this.recheckIfInMenu = false;
    this.subscribeForm = this.formBuilder.group({
      query: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.cartService.cart$.subscribe(cart => {
      this.cartItems = cart?.cartItems?.map((i: any) => {
        return {...i};
      }) ?? [];
      this.totalItems = this.cartItems.reduce((sum, i) => sum + i.quantity, 0);
    });
  }

  removeItem(itemId: number) {
    this.cartService.removeFromCart(itemId);
  }

  sidenavToggle() {
    this.onMenuIconClick.emit();
  }

  ngOnDestroy() {
    this.subscribe1?.unsubscribe();
  }
}
