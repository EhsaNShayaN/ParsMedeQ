import {Component} from '@angular/core';
import {Tables} from '../../../../../core/constants/server.constants';
import {BaseCategoriesComponent} from '../../base-categories.component';

@Component({
  selector: 'app-clip-category-list',
  styleUrls: ['clip-category-list.component.scss'],
  templateUrl: 'clip-category-list.component.html',
  standalone: false
})
export class ClipCategoryListComponent extends BaseCategoriesComponent {
  columnsToDisplay: string[] = [/*'row', */'title', 'creationDate', 'actions'];

  constructor() {
    super(Tables.Clip);
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
