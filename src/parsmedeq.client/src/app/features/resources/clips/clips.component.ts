import {Component} from '@angular/core';
import {BasePageResources} from '../base-page-resources';
import {Tables} from '../../../core/constants/server.constants';

@Component({
  selector: 'app-clips',
  templateUrl: './clips.component.html',
  styleUrls: ['./clips.component.scss'],
  standalone: false,
})
export class ClipsComponent extends BasePageResources {
  constructor() {
    super(Tables.Clip);
  }
}
