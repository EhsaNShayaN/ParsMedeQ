import {Component} from '@angular/core';
import {BaseOurResource} from '../../base-our-resources';
import {Tables} from '../../../../core/constants/server.constants';

@Component({
  selector: 'app-our-clip',
  templateUrl: './our-clip.component.html',
  styleUrl: './our-clip.component.scss',
  standalone: false
})
export class OurclipComponent extends BaseOurResource {
  constructor() {
    super(Tables.Clip);
  }
}
