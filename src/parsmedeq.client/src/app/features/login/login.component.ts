import {Component, ElementRef, EventEmitter, OnDestroy, OnInit, Output} from '@angular/core';
import {UntypedFormBuilder, UntypedFormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {BaseComponent} from '../../base-component';
import {ToastrService} from 'ngx-toastr';
import {MobileFormatterPipe} from '../../core/pipes/mobile-formatter.pipe';
import {AuthService} from '../../core/services/auth.service';
import {first} from 'rxjs/operators';
import {BaseResult} from '../../core/models/BaseResult';
import {SendOtpResponse} from '../../core/models/Login';

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
  error = '';
  ///////////////
  public loginForm: UntypedFormGroup;
  public hide = true;
  private sub: any;
  ///////////////
  @Output() clicked = new EventEmitter<any>();

  constructor(public fb: UntypedFormBuilder, public router: Router, private elementRef: ElementRef,
              private activatedRoute: ActivatedRoute,
              private mobileFormatter: MobileFormatterPipe,
              private toaster: ToastrService,
              private auth: AuthService) {
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
          this.loginFormData['password']?.setValue(d.data.otp)
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
    /*this.baseRestService.verify(this.profileId, this.loginFormData['password']?.value, this.hasCode)
      .pipe(first())
      .subscribe((d: UserResponse) => {
          if (!d) {
            this.error = this.appService.getTranslateValue('UNKNOWN_ERROR');
            this.submitClick = false;
            return;
          }
          if (d.token) {
            this.storageService.removeFromLocalStorage('baskets');
            if (this.clicked?.observed) {
              this.clicked.next(null);
            } else {
              this.navigateToLink(this.returnUrl, 'fa');
            }
          } else if (d.lockOut) {
            this.error = this.appService.getTranslateValue('LOCK_OUT_MESSAGE');
          } else {
            this.error = this.appService.getTranslateValue('PASSWORD_IS_WRONG');
          }
          this.submitClick = false;
        },
        (error: string) => {
          this.error = error;
          this.submitClick = false;
        });*/
  }

  back() {
    this.error = '';
    this.hasMobile = false;
    this.hasCode = 0;
    this.submitClick = false;
    this.loginFormData['password']?.setErrors(null);
  }

  sendOTP(type: number) {
    this.submitClick = true;
    /*this.baseRestService.sendCode(this.profileId, type === 2)
      .pipe(first())
      .subscribe((d: BaseResult<boolean>) => {
        this.submitClick = false;
        this.hasCode = type;
        if (d.data) {
          const msg = 'کد به ' + this.mobile + ' ارسال شد.';
          this.toaster.success(msg, '', {
            // timeOut: 30000,
          });
        } else {
          const msg = 'محدودیت تعداد پیامک در روز';
          this.toaster.error(msg, '', {
            // timeOut: 30000,
          });
        }
      });*/
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
