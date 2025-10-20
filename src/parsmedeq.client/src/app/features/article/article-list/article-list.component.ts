import {Component} from '@angular/core';
import {Router} from '@angular/router';
import {ArticleService} from '../article.service';
import {Article} from '../models/article.model';

@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.scss'],
  standalone: false
})
export class ArticleListComponent {
  articles: Article[] = [];

  constructor(private service: ArticleService, private router: Router) {
    this.articles = this.service.getAll();
  }

  openArticle(article: Article) {
    this.router.navigate(['/articles', article.id]);
  }
}
