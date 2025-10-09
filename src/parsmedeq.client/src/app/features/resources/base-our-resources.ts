import {AfterViewInit, Directive, OnInit} from '@angular/core';
import {Resource, ResourceResponse, ResourcesRequest} from '../../core/models/ResourceResponse';
import {PureComponent} from '../../pure-component';
import {SwiperConfigInterface} from 'ngx-swiper-wrapper';

@Directive()
export class BaseOurResource extends PureComponent implements OnInit, AfterViewInit {
  public items: Resource[] = [];
  public config: SwiperConfigInterface = {};

  constructor(private tableId: number) {
    super();
  }

  ngOnInit() {
    let model: ResourcesRequest = {
      pageIndex: 1,
      pageSize: 10,
      sort: 1,
      tableId: this.tableId
    };
    this.restApiService.getResources(model).subscribe((d: ResourceResponse) => {
      this.items = d.data.items;
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
