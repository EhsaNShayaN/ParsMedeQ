import {Component, Injector} from '@angular/core';
import {Tables} from '../../../../../core/constants/server.constants';
import {BaseResourcesComponent} from '../../base-resources.component';

@Component({
  selector: 'app-clip-list',
  styleUrls: ['clip-list.component.scss'],
  templateUrl: 'clip-list.component.html',
  standalone: false
})
export class ClipListComponent extends BaseResourcesComponent {
  displayedColumns: string[] = [/*'row', */'title', 'publishInfo', 'publisher', 'image', 'creationDate', 'actions'];

  constructor() {
    super(Tables.Clip);
  }

  getColName(column: string) {
    column = column.toLowerCase();
    if (column === 'publishinfo') {
      column = 'PUBLISH_INFO';
    }
    if (column === 'publisher') {
      column = 'PUBLISHER';
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
