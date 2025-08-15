import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-news-detail',
  standalone: false,
  templateUrl: './news-detail.html',
  styleUrl: './news-detail.scss'
})
export class NewsDetail implements OnInit {
  news: any;

  constructor(private route: ActivatedRoute) {
  }

  ngOnInit() {
    const id = this.route.snapshot.params['id'];
    this.news = {
      id,
      title: `خبر شماره ${id}`,
      content: `محتوای کامل خبر شماره ${id}`
    };
  }
}
