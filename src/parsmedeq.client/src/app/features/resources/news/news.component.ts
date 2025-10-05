import {Component, DoCheck, OnInit} from '@angular/core';
import {BasePageResources} from '../base-page-resources';
import {Tables} from '../../../core/constants/server.constants';

@Component({
  selector: 'app-notices',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.scss'],
  standalone: false,
})
export class NewsComponent extends BasePageResources implements OnInit, DoCheck {
  constructor() {
    super(Tables.News);
  }
}
