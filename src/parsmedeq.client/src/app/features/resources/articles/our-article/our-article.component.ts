import {AfterViewInit, Component, OnInit} from '@angular/core';
import {SwiperConfigInterface} from 'ngx-swiper-wrapper';
import {ArticleResponse} from '../../../../../lib/models/ArticleResponse';
import {PureComponent} from '../../../../pure-component';
import {Tables} from "../../../../../lib/core/constants/server.constants";
import {ResourcesRequest} from "../../../../../lib/models/ResourceResponse";

@Component({
  selector: 'app-our-article',
  templateUrl: './our-article.component.html',
  styleUrls: ['./our-article.component.scss']
})
export class OurArticleComponent extends PureComponent implements OnInit, AfterViewInit {
  public items = [];
  public config: SwiperConfigInterface = {};

  ngOnInit() {
    const model: ResourcesRequest = {
      page: 1,
      pageSize: 10,
      sort: 1,
      pinned: true,
      tableId: Tables.Article
    };
    this.restClientService.getResources(model).subscribe((d: ArticleResponse) => {
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
