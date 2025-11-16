import {AfterViewInit, Component, ElementRef} from '@angular/core';
import {BasePageResource} from '../../base-page-resource';
import {Tables} from '../../../../core/constants/server.constants';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-news-details',
  templateUrl: './news-details.component.html',
  styleUrls: ['./news-details.component.scss'],
  standalone: false,
})
export class NewsDetailsComponent extends BasePageResource implements AfterViewInit {
  constructor(private el: ElementRef,
              private toastrService: ToastrService) {
    super(Tables.News);
  }

  override ngAfterViewInit() {
    super.ngAfterViewInit();
    // fade-in the content smoothly
    setTimeout(() => {
      this.el.nativeElement.querySelectorAll('.fade-section').forEach((s: HTMLElement) => s.classList.add('visible'));
    }, 80);
  }

  share() {
    const url = window.location.href;

    if (navigator.share) {
      navigator.share({
        title: document.title,
        text: 'Check this page:',
        url: url
      })
        .catch(err => console.error('Share failed:', err));
    } else {
      // fallback
      navigator.clipboard.writeText(url);
      alert('Share not supported. URL copied to clipboard.');
    }
  }


  copy() {
    const url = window.location.href;

    navigator.clipboard.writeText(url)
      .then(() => {
        console.log('URL copied!');
        this.toastrService.success(this.getTranslateValue('PAGE_URL_COPIED_TO_CLIPBOARD'), '', {});
      })
      .catch(err => {
        console.error('Failed to copy: ', err);
      });
  }

}
