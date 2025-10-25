import {Component, Input} from '@angular/core';
import {BaseComponent} from '../../../base-component';

@Component({
  selector: 'app-panel-comment',
  styleUrls: ['panel-comment.component.scss'],
  templateUrl: 'panel-comment.component.html',
  standalone: false
})
export class PanelCommentComponent extends BaseComponent {
  @Input() url: string = '';
}
