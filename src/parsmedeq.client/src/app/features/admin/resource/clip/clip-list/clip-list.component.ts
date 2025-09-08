import {Component} from '@angular/core';
import {Tables} from '../../../../../core/constants/server.constants';
import {BaseResourcesComponent} from '../../base-resources.component';

@Component({
  selector: 'app-clip-list',
  styleUrls: ['clip-list.component.scss'],
  templateUrl: 'clip-list.component.html',
  standalone: false
})
export class ClipListComponent extends BaseResourcesComponent {
  displayedColumns: string[] = [/*'row', */'title', 'resourceCategoryTitle', 'publishInfo', 'publisher', 'image', 'creationDate', 'actions'];

  constructor() {
    super(Tables.Clip);
  }

  getColName(column: string) {
    column = column.toLowerCase();
    if (column === 'title') {
      column = 'عنوان';
    }
    if (column === 'resourcecategorytitle') {
      column = 'دسته بندی';
    }
    if (column === 'publishinfo') {
      column = 'اطلاعات چاپ';
    }
    if (column === 'publisher') {
      column = 'ناشر';
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
