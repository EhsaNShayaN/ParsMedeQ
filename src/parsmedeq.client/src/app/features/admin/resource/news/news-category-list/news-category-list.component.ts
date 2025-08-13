import {Component, Injector} from '@angular/core';
import {Tables} from '../../../../../core/constants/server.constants';
import {BaseCategoriesComponent} from '../../base-categories.component';

@Component({
  selector: 'app-news-category-list',
  styleUrls: ['news-category-list.component.scss'],
  templateUrl: 'news-category-list.component.html',
  standalone: false
})
export class NewsCategoryListComponent extends BaseCategoriesComponent {
  columnsToDisplay: string[] = [/*'row', */'title', 'parentId', 'creationDate', 'actions'];

  constructor(injector: Injector) {
    super(injector, Tables.News);
  }

  getColName(column: string) {
    column = column.toLowerCase();
    if (column === 'parentid') {
      column = 'news_category';
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
