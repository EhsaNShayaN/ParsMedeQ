import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {ArticleService} from '../article.service';
import {Article} from '../models/article.model';

@Component({
  selector: 'app-article-detail',
  templateUrl: './article-detail.component.html',
  styleUrls: ['./article-detail.component.scss'],
  standalone: false
})
export class ArticleDetailComponent implements OnInit {
  article!: Article;

  constructor(
    private route: ActivatedRoute,
    private service: ArticleService
  ) {
  }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.article = this.service.getById(id);
  }
}
