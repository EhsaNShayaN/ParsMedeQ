import {AfterViewInit, Component, OnInit} from '@angular/core';
import {SwiperConfigInterface} from 'ngx-swiper-wrapper';
import {NoticeResponse} from '../../../../../lib/models/NoticeResponse';
import {PureComponent} from '../../../../pure-component';
import {Tables} from "../../../../../lib/core/constants/server.constants";
import {ResourcesRequest} from "../../../../../lib/models/ResourceResponse";

@Component({
  selector: 'app-our-notice',
  templateUrl: './our-notice.component.html',
  styleUrls: ['./our-notice.component.scss']
})
export class OurNoticeComponent extends PureComponent implements OnInit, AfterViewInit {
  public items = [];
  public config: SwiperConfigInterface = {};
  viewText = 'VIEW_NOTICE';

  ngOnInit() {
    const model: ResourcesRequest = {
      page: 1,
      pageSize: 10,
      sort: 1,
      tableId: Tables.Notice
    };
    this.restClientService.getResources(model).subscribe((d: NoticeResponse) => {
      this.items = d.data.sort((a, b) => a.expired ? 1 : (a.creationDate > b.creationDate ? -1 : 1));
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
