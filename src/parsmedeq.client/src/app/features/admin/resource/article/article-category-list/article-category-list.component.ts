import {Component, Injector} from '@angular/core';
import {Tables} from '../../../../../../lib/core/constants/server.constants';
import {BaseCategoriesComponent} from '../../base-categories.component';

@Component({
  selector: 'app-article-category-list',
  styleUrls: ['article-category-list.component.scss'],
  templateUrl: 'article-category-list.component.html',
  standalone: false
})
export class ArticleCategoryListComponent extends BaseCategoriesComponent {
  columnsToDisplay: string[] = [/*'row', */'title', 'parentId', 'creationDate', 'actions'];

  constructor(injector: Injector) {
    super(injector, Tables.Article);
  }

  getColName(column: string) {
    column = column.toLowerCase();
    if (column === 'parentid') {
      column = 'article_category';
    }
    if (column === 'downloadcount') {
      column = 'download_count';
    }
    if (column === 'expirationdate') {
      column = 'expiration_date';
    }
    if (column === 'creationdate') {
      column = 'published';
    }
    return column.toUpperCase();
  }
}
