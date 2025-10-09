import {AfterViewInit, Component, OnInit} from '@angular/core';
import {SwiperConfigInterface} from 'ngx-swiper-wrapper';
import {ClipResponse} from '../../../../../lib/models/ClipResponse';
import {PureComponent} from '../../../../pure-component';

@Component({
  selector: 'app-our-clip',
  templateUrl: './our-clip.component.html',
  styleUrls: ['./our-clip.component.scss']
})
export class OurclipComponent extends PureComponent implements OnInit, AfterViewInit {
  public items = [];
  public config: SwiperConfigInterface = {};
  viewText = 'VIEW_CLIP';

  ngOnInit() {
    this.restClientService.getClips().subscribe((d: ClipResponse) => {
      this.items = d.data;
    });
  }

  ngAfterViewInit() {
    this.config = {
      observer: true,
      slidesPerView: 4,
      spaceBetween: 16,
      keyboard: false,
      navigation: true,
      pagination: false,
      grabCursor: true,
      loop: false,
      preloadImages: false,
      lazy: true,
      breakpoints: {
        320: {
          slidesPerView: 1
        },
        600: {
          slidesPerView: 2
        },
        960: {
          slidesPerView: 3
        },
        1280: {
          slidesPerView: 4
        }
      }
    };
  }
}
