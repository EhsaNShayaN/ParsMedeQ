import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { News } from './news.model';

@Injectable({ providedIn: 'root' })
export class NewsService {
  private items: News[] = [
    {
      id: 1,
      title: 'معرفی دستگاه جدید پایش علائم حیاتی',
      summary: 'نسل جدید مانیتورینگ بیمار با دقت بالا و قابلیت اتصال به HIS.',
      content: `<p>پارس‌مدیکیو از رونمایی دستگاه جدید مانیتورینگ خود خبر داد. این دستگاه ...</p><p>مزایا شامل دقت بالاتر، مصرف انرژی کمتر و قابلیت اتصال به پایگاه‌های اطلاعاتی بیمارستانی است.</p>`,
      coverImage: 'assets/images/p1.jpg',
      category: 'اخبار محصولات',
      publishDate: '2025-06-12T09:00:00Z'
    },
    {
      id: 2,
      title: 'راهنمای انتخاب مانیتور بیمار برای بخش‌های ICU',
      summary: 'نکاتی برای انتخاب صحیح مانیتور در بخش مراقبت‌های ویژه.',
      content: `<p>در این مقاله به فاکتورهایی می‌پردازیم که هنگام انتخاب مانیتور برای ICU باید در نظر گرفته شوند...</p>`,
      coverImage: 'assets/images/p2.jpg',
      category: 'مقالات',
      publishDate: '2025-05-02T10:30:00Z'
    },
    {
      id: 3,
      title: 'وبینار آموزشی: کالیبراسیون تجهیزات پزشکی',
      summary: 'ثبت نام وبینار رایگان درباره کالیبراسیون تجهیزات تشخیصی و درمانی.',
      content: `<p>در این وبینار متخصصان فنی نحوهٔ کالیبراسیون و نگهداری تجهیزات را آموزش می‌دهند...</p>`,
      coverImage: 'assets/images/p3.jpg',
      category: 'رویدادها',
      publishDate: '2025-04-20T14:00:00Z'
    }
  ];

  list(): Observable<News[]> {
    // return newest first
    return of(this.items.slice().sort((a,b)=> +new Date(b.publishDate) - +new Date(a.publishDate)));
  }

  getById(id: number): Observable<News | undefined> {
    const found = this.items.find(n => n.id === +id);
    // attach some related (simple)
    if (found) {
      const related = this.items.filter(x => x.id !== found.id).slice(0,3);
      return of({ ...found, related});
    }
    return of(undefined);
  }
}
