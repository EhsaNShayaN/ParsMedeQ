import {Component} from '@angular/core';
import {Tables} from '../../../../core/constants/server.constants';
import {BaseOurResource} from '../../base-our-resources';
import {animate, query, stagger, style, transition, trigger} from '@angular/animations';

@Component({
  selector: 'app-our-article',
  templateUrl: './our-article.component.html',
  styleUrl: './our-article.component.scss',
  standalone: false,
  animations: [trigger('articlesAnimation', [
    transition('* => *', [
      query(':enter', [
        style({opacity: 0, transform: 'translateX(-20px)'}),
        stagger(100, [animate('400ms ease-out', style({opacity: 1, transform: 'translateX(0)'}))])
      ], {optional: true})
    ])
  ])]
})
export class OurArticleComponent extends BaseOurResource {
  constructor() {
    super(Tables.Article);
  }
}
