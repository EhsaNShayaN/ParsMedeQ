import {Component} from '@angular/core';

@Component({
  selector: 'app-news',
  standalone: false,
  templateUrl: './news.html',
  styleUrl: './news.scss'
})
export class News {
  newsList = [
    {id: 1, title: 'خبر شماره یک', summary: 'خلاصه خبر شماره یک'},
    {id: 2, title: 'خبر شماره دو', summary: 'خلاصه خبر شماره دو'}
  ];
}
