import {Component, Injector} from '@angular/core';
import {Tables} from '../../../../../core/constants/server.constants';
import {BaseResourcesComponent} from '../../base-resources.component';

@Component({
  selector: 'app-article-list',
  styleUrls: ['article-list.component.scss'],
  templateUrl: 'article-list.component.html',
  standalone: false
})
export class ArticleListComponent extends BaseResourcesComponent {
  displayedColumns: string[] = [/*'row', */'title', 'resourceCategoryTitle', 'downloadCount', 'image', 'creationDate', 'actions'];

  constructor() {
    super(Tables.Article);
  }

  getColName(column: string) {
    column = column.toLowerCase();
    if (column === 'title') {
      column = 'عنوان';
    }
    if (column === 'resourcecategorytitle') {
      column = 'دسته بندی';
    }
    if (column === 'downloadcount') {
      column = 'تعداد دانلود';
    }
    if (column === 'expirationdate') {
      column = 'تاریخ انقضا';
    }
    if (column === 'image') {
      column = 'تصویر';
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
