import {AfterViewInit, Component, OnInit} from '@angular/core';
import {SwiperConfigInterface} from 'ngx-swiper-wrapper';
import {PureComponent} from '../../../pure-component';
import {ProductCategoriesResponse} from '../../../../lib/models/ProductResponse';

@Component({
  selector: 'app-our-product',
  templateUrl: './our-product.component.html',
  styleUrls: ['./our-product.component.scss']
})
export class OurProductComponent extends PureComponent implements OnInit, AfterViewInit {
  public items = [];
  public config: SwiperConfigInterface = {};
  viewText = 'نمایش محصول';

  ngOnInit() {
    const model = {
      page: 1,
      pageSize: -1,
      sort: 1,
      isHome: true,
      url: 'general'
    };
    this.restClientService.getProductCategories().subscribe((d: ProductCategoriesResponse) => {
      this.items = d.productCategories.filter(s => !s.parentId);
    });
  }

  func(a, b) {
    return 0.5 - Math.random();
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
