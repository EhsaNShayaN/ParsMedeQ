import {Component} from '@angular/core';
import {BasePageResources} from '../base-page-resources';
import {Tables} from '../../../core/constants/server.constants';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrl: './articles.component.scss',
  standalone: false,
})
export class ArticlesComponent extends BasePageResources {
  constructor() {
    super(Tables.Article);
  }
}
