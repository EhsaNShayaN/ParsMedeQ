import {Injectable} from '@angular/core';
import {Observable, of} from 'rxjs';
import {Clip} from './models/clip.model';

@Injectable({providedIn: 'root'})
export class ClipService {
  private clips: Clip[] = [
    {
      id: 1,
      title: 'آموزش کار با مانیتور PM-200',
      description: 'راهنمای گام به گام اتصال و خواندن پارامترهای حیاتی.',
      thumbnail: 'assets/images/p1.jpg',
      videoUrl: 'assets/images/p1.mp4',
      category: 'آموزش تجهیزات',
      publishDate: '2025-06-01T10:00:00Z',
      tags: ['مانیتور', 'آموزش', 'PM-200']
    },
    {
      id: 2,
      title: 'کالیبراسیون فشارسنج بیمارستانی',
      description: 'روش صحیح کالیبراسیون دستگاه فشارسنج در بخش‌های ICU.',
      thumbnail: 'assets/images/p2.jpg',
      videoUrl: 'assets/images/p2.mp4',
      category: 'آموزش فنی',
      publishDate: '2025-05-20T09:30:00Z',
      tags: ['کالیبراسیون', 'فنی']
    },
    {
      id: 3,
      title: 'نحوه پاکسازی و استریلیزاسیون تجهیزات',
      description: 'مراقبت و نگهداری روزمره برای افزایش عمر تجهیزات.',
      thumbnail: 'assets/images/p3.jpg',
      videoUrl: 'assets/images/p3.mp4',
      category: 'آموزش بهداشت',
      publishDate: '2025-04-12T12:00:00Z',
      tags: ['استریلیزاسیون', 'نگهداری']
    }
  ];

  list(): Observable<Clip[]> {
    return of(this.clips.slice().sort((a, b) => +new Date(b.publishDate) - +new Date(a.publishDate)));
  }

  getById(id: number): Observable<Clip | undefined> {
    const found = this.clips.find(c => c.id === +id);
    if (!found) return of(undefined);
    // attach related: others except current
    const related = this.clips.filter(x => x.id !== found.id).slice(0, 3);
    return of({...found, related});
  }
}
