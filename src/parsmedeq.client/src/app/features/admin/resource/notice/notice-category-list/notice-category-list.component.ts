import {Component, Injector} from '@angular/core';
import {Tables} from '../../../../../core/constants/server.constants';
import {BaseCategoriesComponent} from '../../base-categories.component';

@Component({
  selector: 'app-notice-category-list',
  styleUrls: ['notice-category-list.component.scss'],
  templateUrl: 'notice-category-list.component.html',
  standalone: false
})
export class NoticeCategoryListComponent extends BaseCategoriesComponent {
  columnsToDisplay: string[] = [/*'row', */'title', 'parentId', 'creationDate', 'actions'];

  constructor(injector: Injector) {
    super(injector, Tables.Notice);
  }

  getColName(column: string) {
    column = column.toLowerCase();
    if (column === 'parentid') {
      column = 'notice_category';
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
