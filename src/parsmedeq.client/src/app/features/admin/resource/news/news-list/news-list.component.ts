import {Component, Injector} from '@angular/core';
import {Tables} from '../../../../../core/constants/server.constants';
import {BaseResourcesComponent} from '../../base-resources.component';

@Component({
  selector: 'app-news-list',
  styleUrl: 'news-list.component.scss',
  templateUrl: 'news-list.component.html',
  standalone: false
})
export class NewsListComponent extends BaseResourcesComponent {
  displayedColumns: string[] = [/*'row', */'title', 'categoryTitle', 'downloadCount', 'image', 'creationDate', 'actions'];

  constructor(injector: Injector) {
    super(injector, Tables.News);
  }

  getColName(column: string) {
    column = column.toLowerCase();
    if (column === 'categorytitle') {
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
