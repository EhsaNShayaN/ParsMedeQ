import {Component, Input} from '@angular/core';
import {BaseComponent} from '../../../base-component';

@Component({
  selector: 'app-panel-ticket',
  styleUrls: ['panel-ticket.component.scss'],
  templateUrl: 'panel-ticket.component.html',
  standalone: false
})
export class PanelTicketComponent extends BaseComponent {
  @Input() url: string = '';
}
