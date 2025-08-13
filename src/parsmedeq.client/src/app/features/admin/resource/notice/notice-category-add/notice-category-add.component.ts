import {Component, Injector} from '@angular/core';
import {Tables} from '../../../../../core/constants/server.constants';
import {BaseCategoryComponent} from '../../base-category.component';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-notice-category-add',
  templateUrl: './notice-category-add.component.html',
  styleUrl: './notice-category-add.component.scss',
  standalone: false
})
export class NoticeCategoryAddComponent extends BaseCategoryComponent {
  constructor(private aRoute: ActivatedRoute, injector: Injector) {
    super(injector, Tables.Notice, aRoute);
  }
}
