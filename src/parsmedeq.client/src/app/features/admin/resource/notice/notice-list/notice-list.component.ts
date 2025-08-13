import {Component, Injector} from '@angular/core';
import {Tables} from '../../../../../core/constants/server.constants';
import {BaseResourcesComponent} from '../../base-resources.component';

@Component({
  selector: 'app-notice-list',
  styleUrl: 'notice-list.component.scss',
  templateUrl: 'notice-list.component.html',
  standalone: false
})
export class NoticeListComponent extends BaseResourcesComponent {
  displayedColumns: string[] = [/*'row', */'title', 'categoryTitle', 'downloadCount', 'image', 'creationDate', 'actions'];

  constructor(injector: Injector) {
    super(injector, Tables.Notice);
  }

  getColName(column: string) {
    column = column.toLowerCase();
    if (column === 'categorytitle') {
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
