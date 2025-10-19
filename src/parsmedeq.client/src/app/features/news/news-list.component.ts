import {Component, OnInit, ElementRef, AfterViewInit} from '@angular/core';
import {NewsService} from './news.service';
import {News} from './news.model';
import {Router} from '@angular/router';

@Component({
  selector: 'app-news-list',
  templateUrl: './news-list.component.html',
  styleUrls: ['./news-list.component.scss'],
  standalone: false
})
export class NewsListComponent implements OnInit, AfterViewInit {
  items: News[] = [];
  loading = true;

  constructor(
    private newsService: NewsService,
    private router: Router,
    private el: ElementRef
  ) {
  }

  ngOnInit(): void {
    this.newsService.list().subscribe(res => {
      this.items = res;
      this.loading = false;
    });
  }

  open(n: News) {
    this.router.navigate(['/news', n.id]);
  }

  ngAfterViewInit() {
    // simple fade-in on scroll using IntersectionObserver
    const io = new IntersectionObserver((entries) => {
      entries.forEach(e => {
        if (e.isIntersecting) {
          e.target.classList.add('visible');
          io.unobserve(e.target);
        }
      });
    }, {threshold: 0.15});

    this.el.nativeElement.querySelectorAll('.news-card').forEach((el: HTMLElement) => io.observe(el));
  }

  formatDate(iso: string) {
    try {
      const d = new Date(iso);
      return d.toLocaleDateString('fa-IR', {year: 'numeric', month: 'long', day: 'numeric'});
    } catch {
      return iso;
    }
  }
}
