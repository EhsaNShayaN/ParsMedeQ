import {Component, Input, OnDestroy} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {BaseComponent} from '../../../../base-component';
import {Order, OrderResponse} from '../../../../core/models/OrderResponse';
import {OrderStatus} from '../../../../core/constants/server.constants';
import {OrderService} from '../../../../core/services/rest/order-service';
import {Helpers} from '../../../../core/helpers';

@Component({
  selector: 'app-panel-order-details',
  templateUrl: './panel-order-details.component.html',
  styleUrls: ['./panel-order-details.component.scss'],
  standalone: false
})
export class PanelOrderDetailsComponent extends BaseComponent implements OnDestroy {
  @Input() url: string = '';
  sub: any;
  order: Order | undefined;
  protected readonly OrderStatus = OrderStatus;

  constructor(private activatedRoute: ActivatedRoute,
              private orderService: OrderService,
              protected helpers: Helpers) {
    super();
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.orderService.getOrder(params['id']).subscribe((a: OrderResponse) => {
        this.order = a.data;
      });
    });
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }
}
