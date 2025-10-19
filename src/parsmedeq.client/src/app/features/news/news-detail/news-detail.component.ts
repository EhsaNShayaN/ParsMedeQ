import {Component, OnInit, ElementRef, AfterViewInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {NewsService} from '../news.service';
import {News} from '../models/news.model';
import {BaseComponent} from '../../../base-component';

@Component({
  selector: 'app-news-detail',
  templateUrl: './news-detail.component.html',
  styleUrls: ['./news-detail.component.scss'],
  standalone: false
})
export class NewsDetailComponent extends BaseComponent implements OnInit, AfterViewInit {
  item?: News;
  loading = true;

  constructor(private route: ActivatedRoute,
              private newsService: NewsService,
              private el: ElementRef) {
    super();
  }

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.newsService.getById(id).subscribe(n => {
      this.item = n;
      this.loading = false;
    });
  }

  ngAfterViewInit() {
    // fade-in the content smoothly
    setTimeout(() => {
      this.el.nativeElement.querySelectorAll('.fade-section').forEach((s: HTMLElement) => s.classList.add('visible'));
    }, 80);
  }

  back() {
    this.router.navigate(['/news']);
  }

  formatDate(iso: string) {
    try {
      const d = new Date(iso);
      return d.toLocaleDateString('fa-IR', {year: 'numeric', month: 'long', day: 'numeric'});
    } catch {
      return iso;
    }
  }

  share(platform: string) {
    // placeholder: implement actual share URLs if needed
    window.alert(`اشتراک‌گذاری در ${platform} (فعلاً نمونه)`);
  }
}
