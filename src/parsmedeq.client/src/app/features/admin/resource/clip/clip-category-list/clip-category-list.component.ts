import {Component, Injector} from '@angular/core';
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

  constructor(injector: Injector) {
    super(injector, Tables.Clip);
  }

  getColName(column: string) {
    column = column.toLowerCase();
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
