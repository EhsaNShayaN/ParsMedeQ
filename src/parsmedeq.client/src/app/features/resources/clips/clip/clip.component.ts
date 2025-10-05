import {Component} from '@angular/core';
import {BasePageResource} from '../../base-page-resource';
import {Tables} from '../../../../core/constants/server.constants';

@Component({
  selector: 'app-clip',
  templateUrl: './clip.component.html',
  styleUrls: ['./clip.component.scss'],
  standalone: false,
})
export class ClipComponent extends BasePageResource {
  constructor() {
    super(Tables.Clip);
  }
}
