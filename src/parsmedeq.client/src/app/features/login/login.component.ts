import {Component, EventEmitter, OnDestroy, OnInit, Output} from '@angular/core';
import {UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import {BaseComponent} from '../../base-component';
import {MobileFormatterPipe} from '../../core/pipes/mobile-formatter.pipe';
import {AuthService} from '../../core/services/auth.service';
import {first} from 'rxjs/operators';
import {BaseResult} from '../../core/models/BaseResult';
import {MobileResponse, SendOtpResponse} from '../../core/models/Login';
import {CartService} from '../../core/services/cart.service';
import {StorageService} from '../../core/services/storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  standalone: false
})
export class LoginComponent extends BaseComponent implements OnInit, OnDestroy {
  hasMobile = false;
  hasCode = 0;
  mobile = '';
  submitClick = false;
  submitted = false;
  returnUrl = '';
  error: string = '';
  ///////////////
  public loginForm: UntypedFormGroup;
  public hide = true;
  private sub: any;
  ///////////////
  @Output() clicked = new EventEmitter<any>();

  constructor(public fb: UntypedFormBuilder,
              private activatedRoute: ActivatedRoute,
              private mobileFormatter: MobileFormatterPipe,
              private auth: AuthService,
              private storageService: StorageService,
              private cartService: CartService) {
    super();
    this.loginForm = this.fb.group({
      username: ['', Validators.compose([Validators.required, Validators.minLength(10)])],
      password: '',
      rememberMe: false
    });
  }

  ngOnInit() {
    this.sub = this.activatedRoute.params.subscribe(params => {
      if (params['id']) {
        this.returnUrl = decodeURI(params['id']);
      }
      if (this.auth.isLoggedIn()) {
        this.navigateToLink(this.returnUrl, 'fa');
      }
    });
  }

  get loginFormData() {
    return this.loginForm.controls;
  }

  public onLoginFormSubmit(values: any): void {
    this.submitted = true;
    if (this.loginForm.invalid) {
      console.log('invalid');
      return;
    }
    this.submitClick = true;
    if (this.hasMobile) {
      this.onVerify();
      return;
    }
    this.mobile = this.mobileFormatter.transform(this.loginFormData['username']?.value);
    this.restApiService.sendOtp({mobile: this.loginFormData['username']?.value})
      .pipe(first())
      .subscribe((d: BaseResult<SendOtpResponse>) => {
          this.submitClick = false;
          this.hasMobile = true;
          this.loginFormData['password']?.setValue(d.data.otp);
        },
        (error: string) => {
          this.error = error;
          this.submitClick = false;
        });
  }

  onVerify() {
    if (!this.loginFormData['password']?.value) {
      this.loginFormData['password']?.setErrors({required: true});
      this.submitClick = false;
      return;
    }
    this.loginFormData['password']?.clearValidators();
    this.restApiService.sendMobile({mobile: this.loginFormData['username']?.value, otp: this.loginFormData['password']?.value})
      .pipe(first()).subscribe((d: BaseResult<MobileResponse>) => {
        if (!d) {
          this.error = this.getTranslateValue('UNKNOWN_ERROR');
          this.submitClick = false;
          return;
        }
        if (d.data.token) {
          this.auth.login(d.data.token);
          this.navigateToLink(this.returnUrl, 'fa');
          this.cartService.mergeCart();
          this.storageService.clearAnonymousId();
        } else {
          this.error = this.getTranslateValue('PASSWORD_IS_WRONG');
        }
        this.submitClick = false;
      },
      (error: string) => {
        this.error = error;
        this.submitClick = false;
      });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
