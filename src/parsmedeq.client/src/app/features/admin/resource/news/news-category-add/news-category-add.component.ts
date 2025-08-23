import {Component, Injector} from '@angular/core';
import {Tables} from '../../../../../core/constants/server.constants';
import {BaseCategoryComponent} from '../../base-category.component';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-news-category-add',
  templateUrl: './news-category-add.component.html',
  styleUrl: './news-category-add.component.scss',
  standalone: false
})
export class NewsCategoryAddComponent extends BaseCategoryComponent {
  constructor(private aRoute: ActivatedRoute) {
    super(Tables.News, aRoute);
  }
}
