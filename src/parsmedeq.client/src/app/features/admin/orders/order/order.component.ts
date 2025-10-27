import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {BaseComponent} from '../../../../base-component';
import {Order, OrderResponse} from '../../../../core/models/AddOrderResponse';
import {OrderStatus} from '../../../../core/constants/server.constants';
import * as moment from 'jalali-moment';
import {OrderService} from '../../../../core/services/rest/order-service';

@Component({
  selector: 'app-order-finish',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss'],
  standalone: false
})
export class OrderComponent extends BaseComponent implements OnInit, OnDestroy {
  sub: any;
  order: Order | undefined;
  protected readonly OrderStatus = OrderStatus;

  constructor(private activatedRoute: ActivatedRoute,
              private orderService: OrderService) {
    super();
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.orderService.getOrder(params['id']).subscribe((a: OrderResponse) => {
        this.order = a.data;
      });
    });
  }

  ngOnInit(): void {
  }

  convertToPersianDate(dateStr: string): string {
    const persianDate = moment.from(dateStr, 'YYYY-MM-DD')
      .locale('fa')          // switch to Persian locale
      .format('jYYYY/jMM/jDD'); // use "j" for Jalali calendar
    const date = new Date(dateStr);
    const time = date.toLocaleTimeString('fa-IR', {hour: '2-digit', minute: '2-digit'});
    return `${persianDate}-${time}`;
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }
}
