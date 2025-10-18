import {Component} from '@angular/core';
import {BaseOurResource} from '../../base-our-resources';
import {Tables} from '../../../../core/constants/server.constants';
import {animate, query, stagger, style, transition, trigger} from '@angular/animations';

@Component({
  selector: 'app-our-clip',
  templateUrl: './our-clip.component.html',
  styleUrl: './our-clip.component.scss',
  standalone: false,
  animations: [trigger('clipsAnimation', [
    transition('* => *', [
      query(':enter', [
        style({opacity: 0, transform: 'scale(0.85)'}),
        stagger(100, [animate('400ms ease-out', style({opacity: 1, transform: 'scale(1)'}))])
      ], {optional: true})
    ])
  ])]
})
export class OurClipComponent extends BaseOurResource {
  constructor() {
    super(Tables.Clip);
  }
}
