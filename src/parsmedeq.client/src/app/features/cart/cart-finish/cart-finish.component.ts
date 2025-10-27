import {Component, OnDestroy, OnInit} from '@angular/core';
import {BaseComponent} from '../../../base-component';
import {ActivatedRoute} from '@angular/router';
import {first} from 'rxjs/operators';
import {ConfirmPayment, ConfirmPaymentResponse} from '../../../core/models/ConfirmPaymentResponse';
import {PaymentService} from '../../../core/services/rest/payment-service';

@Component({
  selector: 'app-cart-finish',
  templateUrl: './cart-finish.component.html',
  styleUrls: ['./cart-finish.component.scss'],
  standalone: false
})
export class CartFinishComponent extends BaseComponent implements OnInit, OnDestroy {
  sub: any;
  paymentId: number = 0;
  status: 'success' | 'error' | 'pending' = 'pending';
  message: string = '';
  paymentResult: ConfirmPayment | null = null;

  constructor(private activatedRoute: ActivatedRoute,
              private paymentService: PaymentService) {
    super();
  }

  ngOnInit(): void {
    this.sub = this.activatedRoute.params.subscribe(async params => {
      this.paymentId = params['id'];
      //await this.delay(5000); // تأخیر ۲ ثانیه‌ای
      this.confirmPayment();
    });
  }

  confirmPayment() {
    this.paymentService.confirmPayment(this.paymentId, this.generateOT()).pipe(first()).subscribe((d: ConfirmPaymentResponse) => {
      this.paymentResult = d.data;
      if (d.data.transactionId) {
        this.status = 'success';
        this.message = this.getTranslateValue('THE_OPERATION_WAS_SUCCESSFUL');
      } else {
        this.status = 'error';
        this.message = this.getTranslateValue('UNKNOWN_ERROR');
      }
    });
  }

  generateOT() {
    return Math.floor(100000 + Math.random() * 900000).toString();
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }

  goToOrder() {
    const currentLang = this.translateService.getDefaultLang();
    this.navigateToLink(`/order/${this.paymentResult?.orderId}`, currentLang);
  }

  retryPayment() {

  }

  downloadReceipt() {

  }
}
