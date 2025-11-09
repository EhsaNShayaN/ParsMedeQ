import {Component, Input} from '@angular/core';
import {BaseComponent} from '../../../base-component';

@Component({
  selector: 'app-panel-periodic-service',
  styleUrls: ['panel-periodic-service.component.scss'],
  templateUrl: 'panel-periodic-service.component.html',
  standalone: false
})
export class PanelPeriodicServiceComponent extends BaseComponent {
  @Input() url: string = '';
}
