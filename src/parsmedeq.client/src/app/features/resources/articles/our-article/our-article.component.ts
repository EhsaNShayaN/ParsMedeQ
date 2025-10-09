import {Component} from '@angular/core';
import {Tables} from '../../../../core/constants/server.constants';
import {BaseOurResource} from '../../base-our-resources';

@Component({
  selector: 'app-our-article',
  templateUrl: './our-article.component.html',
  styleUrl: './our-article.component.scss',
  standalone: false
})
export class OurArticleComponent extends BaseOurResource {
  constructor() {
    super(Tables.Article);
  }
}
