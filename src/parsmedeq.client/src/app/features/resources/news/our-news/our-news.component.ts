import {Component} from '@angular/core';
import {BaseOurResource} from '../../base-our-resources';
import {Tables} from '../../../../core/constants/server.constants';

@Component({
  selector: 'app-our-news',
  templateUrl: './our-news.component.html',
  styleUrl: './our-news.component.scss',
  standalone: false
})
export class OurNewsComponent extends BaseOurResource {
  constructor() {
    super(Tables.News);
  }
}
