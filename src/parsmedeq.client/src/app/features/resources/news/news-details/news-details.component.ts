import {AfterViewInit, Component, ElementRef} from '@angular/core';
import {BasePageResource} from '../../base-page-resource';
import {Tables} from '../../../../core/constants/server.constants';

@Component({
  selector: 'app-news-details',
  templateUrl: './news-details.component.html',
  styleUrls: ['./news-details.component.scss'],
  standalone: false,
})
export class NewsDetailsComponent extends BasePageResource implements AfterViewInit {
  constructor(private el: ElementRef) {
    super(Tables.News);
  }

  override ngAfterViewInit() {
    super.ngAfterViewInit();
    // fade-in the content smoothly
    setTimeout(() => {
      this.el.nativeElement.querySelectorAll('.fade-section').forEach((s: HTMLElement) => s.classList.add('visible'));
    }, 80);
  }

  share(target: string) {

  }
}
