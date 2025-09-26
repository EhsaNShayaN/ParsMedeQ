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
  displayedColumns: string[] = [/*'row', */'title', 'resourceCategoryTitle', 'price', 'discount', 'image', 'creationDate', 'actions'];

  constructor() {
    super(Tables.Clip);
  }

  getColName(column: string) {
    column = column.toLowerCase();
    if (column === 'resourcecategorytitle') {
      column = 'category';
    }
    if (column === 'creationdate') {
      column = 'creation_date';
    }
    return this.getTranslateValue(column.toUpperCase());
  }
}
