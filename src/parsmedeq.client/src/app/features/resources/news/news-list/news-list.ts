import {Component} from '@angular/core';

@Component({
  selector: 'app-news',
  standalone: false,
  templateUrl: './news-list.html',
  styleUrl: './news-list.scss'
})
export class NewsList {
  newsList = [
    {id: 1, title: 'خبر شماره یک', summary: 'خلاصه خبر شماره یک'},
    {id: 2, title: 'خبر شماره دو', summary: 'خلاصه خبر شماره دو'}
  ];
}
