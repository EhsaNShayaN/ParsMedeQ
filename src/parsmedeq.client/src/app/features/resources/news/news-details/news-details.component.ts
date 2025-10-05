import {Component} from '@angular/core';
import {BasePageResource} from '../../base-page-resource';
import {Tables} from '../../../../core/constants/server.constants';

@Component({
  selector: 'app-news-details',
  templateUrl: './news-details.component.html',
  styleUrls: ['./news-details.component.scss'],
  standalone: false,
})
export class NewsDetailsComponent extends BasePageResource {
  constructor() {
    super(Tables.News);
  }
}
