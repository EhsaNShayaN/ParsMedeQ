import {Component, Injector} from '@angular/core';
import {Tables} from '../../../../../core/constants/server.constants';
import {BaseCategoryComponent} from '../../base-category.component';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-clip-category-add',
  templateUrl: './clip-category-add.component.html',
  styleUrls: ['./clip-category-add.component.scss'],
  standalone: false
})
export class ClipCategoryAddComponent extends BaseCategoryComponent {
  constructor(private aRoute: ActivatedRoute, injector: Injector) {
    super(injector, Tables.Clip, aRoute);
  }
}
