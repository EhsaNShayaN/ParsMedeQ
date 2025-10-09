import {AfterViewInit, Component, OnInit} from '@angular/core';
import {SwiperConfigInterface} from 'ngx-swiper-wrapper';
import {PureComponent} from '../../../pure-component';
import {Product, ProductResponse, ProductsRequest} from '../../../core/models/ProductResponse';

@Component({
  selector: 'app-our-product',
  templateUrl: './our-product.component.html',
  styleUrl: './our-product.component.scss',
  standalone: false
})
export class OurProductComponent extends PureComponent implements OnInit, AfterViewInit {
  public items: Product[] = [];
  public config: SwiperConfigInterface = {};

  ngOnInit() {
    let model: ProductsRequest = {
      pageIndex: 1,
      pageSize: 10,
      sort: 1,
    };
    this.restApiService.getProducts(model).subscribe((d: ProductResponse) => {
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
