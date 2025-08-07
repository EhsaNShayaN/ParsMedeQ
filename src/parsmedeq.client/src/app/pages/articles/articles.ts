import {Component} from '@angular/core';

export interface Article {
  id: number,
  title: string,
  summary: string,
  comments: string[],
}

@Component({
  selector: 'app-articles',
  templateUrl: './articles.html',
  styleUrl: './articles.scss',
  standalone: false
})
export class Articles {
  articles: Article[] = [
    {
      id: 1,
      title: 'نقش تجهیزات پزشکی در جراحی‌های نوین',
      summary: 'تجهیزات پیشرفته پزشکی در بهبود نتایج جراحی نقش مهمی دارند...',
      comments: []
    },
    {
      id: 2,
      title: 'راهنمای خرید دستگاه ECG',
      summary: 'در این مقاله به نکاتی برای خرید دستگاه نوار قلب اشاره خواهیم کرد...',
      comments: []
    }
  ];

  newComment: string = '';

  addComment(articleId: number) {
    const article = this.articles.find(a => a.id === articleId);
    if (article && this.newComment.trim()) {
      article.comments.push(this.newComment.trim());
      this.newComment = '';
    }
  }
}
