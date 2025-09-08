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

  constructor() {
    super(Tables.News);
  }

  getColName(column: string) {
    column = column.toLowerCase();
    if (column === 'title') {
      column = 'عنوان';
    }
    if (column === 'parentid') {
      column = 'دسته بندی';
    }
    if (column === 'creationdate') {
      column = 'تاریخ ایجاد';
    }
    if (column === 'actions') {
      column = 'عملیات';
    }
    return column.toUpperCase();
  }
}
