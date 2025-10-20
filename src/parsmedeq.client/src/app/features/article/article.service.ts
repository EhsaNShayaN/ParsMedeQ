import {Injectable} from '@angular/core';
import {Article} from './models/article.model';

@Injectable({providedIn: 'root'})
export class ArticleService {
  private articles: Article[] = [
    {
      id: 1,
      title: 'آشنایی با تکنولوژی‌های جدید پزشکی',
      author: 'دکتر رضایی',
      publishDate: '2025-09-20',
      summary: 'مروری بر فناوری‌های نوین در حوزه تجهیزات پزشکی و تاثیر آن‌ها بر درمان‌های مدرن.',
      content: `
        <p>در دهه‌ی اخیر، پیشرفت‌های چشمگیری در حوزه‌ی مهندسی پزشکی رخ داده است. از هوش مصنوعی گرفته تا دستگاه‌های قابل‌پوشیدن...</p>
        <p>در این مقاله به بررسی تکنولوژی‌های نوین از جمله تصویربرداری دقیق‌تر و جراحی رباتیک خواهیم پرداخت.</p>
      `,
      thumbnail: '/assets/images/p1.jpg',
      related: [
        {
          id: 2,
          title: 'تحلیل داده‌های پزشکی با هوش مصنوعی',
          author: 'مهندس نادری',
          publishDate: '2025-08-11',
          summary: '',
          content: '',
          thumbnail: '/assets/images/p2.jpg'
        }
      ]
    },
    {
      id: 2,
      title: 'تحلیل داده‌های پزشکی با هوش مصنوعی',
      author: 'مهندس نادری',
      publishDate: '2025-08-11',
      summary: 'بررسی کاربرد AI در تحلیل تصاویر و داده‌های بالینی برای بهبود دقت تشخیص.',
      content: `
        <p>هوش مصنوعی امروز نقش مهمی در دنیای پزشکی ایفا می‌کند...</p>
      `,
      thumbnail: '/assets/images/p2.jpg'
    },
    {
      id: 3,
      title: 'آشنایی با تکنولوژی‌های جدید پزشکی',
      author: 'دکتر رضایی',
      publishDate: '2025-09-20',
      summary: 'مروری بر فناوری‌های نوین در حوزه تجهیزات پزشکی و تاثیر آن‌ها بر درمان‌های مدرن.',
      content: `
        <p>در دهه‌ی اخیر، پیشرفت‌های چشمگیری در حوزه‌ی مهندسی پزشکی رخ داده است. از هوش مصنوعی گرفته تا دستگاه‌های قابل‌پوشیدن...</p>
        <p>در این مقاله به بررسی تکنولوژی‌های نوین از جمله تصویربرداری دقیق‌تر و جراحی رباتیک خواهیم پرداخت.</p>
      `,
      thumbnail: '/assets/images/p3.jpg',
      related: [
        {
          id: 2,
          title: 'تحلیل داده‌های پزشکی با هوش مصنوعی',
          author: 'مهندس نادری',
          publishDate: '2025-08-11',
          summary: '',
          content: '',
          thumbnail: '/assets/images/p3.jpg'
        }
      ]
    },
  ];

  getAll(): Article[] {
    return this.articles;
  }

  getById(id: number): Article {
    return this.articles.find(a => a.id === id)!;
  }
}
