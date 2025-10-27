import {Component, Input} from '@angular/core';
import {BaseComponent} from '../../../base-component';

@Component({
  selector: 'app-panel-order',
  styleUrls: ['panel-order.component.scss'],
  templateUrl: 'panel-order.component.html',
  standalone: false
})
export class PanelOrderComponent extends BaseComponent {
  @Input() url: string = '';
}
