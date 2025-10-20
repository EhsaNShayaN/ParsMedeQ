import {AfterViewInit, Component, ElementRef} from '@angular/core';
import {BasePageResources} from '../base-page-resources';
import {Tables} from '../../../core/constants/server.constants';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.scss'],
  standalone: false
})
export class NewsComponent extends BasePageResources implements AfterViewInit {
  constructor(private el: ElementRef) {
    super(Tables.News);
  }

  ngAfterViewInit() {
    const io = new IntersectionObserver((entries) => {
      entries.forEach(e => {
        console.log('entries', e.isIntersecting);
        if (e.isIntersecting) {
          e.target.classList.add('visible');
          io.unobserve(e.target);
        }
      });
    }, {threshold: 0.15});
    this.el.nativeElement.querySelectorAll('.news-card').forEach((el: HTMLElement) => io.observe(el));
  }
}
