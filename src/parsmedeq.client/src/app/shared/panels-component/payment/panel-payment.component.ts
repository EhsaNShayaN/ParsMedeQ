import {Component, Input} from '@angular/core';
import {BaseComponent} from '../../../base-component';

@Component({
  selector: 'app-panel-payment',
  styleUrls: ['panel-payment.component.scss'],
  templateUrl: 'panel-payment.component.html',
  standalone: false
})
export class PanelPaymentComponent extends BaseComponent {
  @Input() url: string = '';
}
