import {Component} from '@angular/core';
import {Tables} from '../../../../core/constants/server.constants';
import {BasePageResource} from '../../base-page-resource';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss'],
  standalone: false,
})
export class ArticleComponent extends BasePageResource {
  constructor() {
    super(Tables.Article);
  }
}
