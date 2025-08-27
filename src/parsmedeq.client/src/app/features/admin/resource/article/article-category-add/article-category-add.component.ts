import {Component} from '@angular/core';
import {Tables} from '../../../../../core/constants/server.constants';
import {BaseCategoryComponent} from '../../base-category.component';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-article-category-add',
  templateUrl: './article-category-add.component.html',
  styleUrls: ['./article-category-add.component.scss'],
  standalone: false
})
export class ArticleCategoryAddComponent extends BaseCategoryComponent {
  constructor(private aRoute: ActivatedRoute) {
    super(Tables.Article, aRoute);
  }
}
