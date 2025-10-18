import {Component} from '@angular/core';
import {BaseOurResource} from '../../base-our-resources';
import {Tables} from '../../../../core/constants/server.constants';
import {animate, query, stagger, style, transition, trigger} from '@angular/animations';

@Component({
  selector: 'app-our-news',
  templateUrl: './our-news.component.html',
  styleUrl: './our-news.component.scss',
  standalone: false,
  animations: [trigger('newsAnimation', [
    transition('* => *', [
      query(':enter', [
        style({opacity: 0}),
        stagger(150, [animate('400ms ease-out', style({opacity: 1}))])
      ], {optional: true})
    ])
  ])]
})
export class OurNewsComponent extends BaseOurResource {
  constructor() {
    super(Tables.News);
  }
}
