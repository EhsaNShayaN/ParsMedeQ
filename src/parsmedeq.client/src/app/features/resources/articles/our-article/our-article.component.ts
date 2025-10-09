import {AfterViewInit, Component, OnInit} from '@angular/core';
import {SwiperConfigInterface} from 'ngx-swiper-wrapper';
import {PureComponent} from '../../../../pure-component';
import {Resource, ResourceResponse, ResourcesRequest} from '../../../../core/models/ResourceResponse';
import {Tables} from '../../../../core/constants/server.constants';

@Component({
  selector: 'app-our-article',
  templateUrl: './our-article.component.html',
  styleUrl: './our-article.component.scss',
  standalone: false
})
export class OurArticleComponent extends PureComponent implements OnInit, AfterViewInit {
  public items: Resource[] = [];
  public config: SwiperConfigInterface = {};

  ngOnInit() {
    let model: ResourcesRequest = {
      pageIndex: 1,
      pageSize: 10,
      sort: 1,
      tableId: Tables.Article
    };
    this.restApiService.getResources(model).subscribe((d: ResourceResponse) => {
      this.items.push(d.data.items[0]);
      this.items.push(d.data.items[0]);
      this.items.push(d.data.items[0]);
      this.items.push(d.data.items[0]);
      this.items.push(d.data.items[0]);
      this.items.push(d.data.items[0]);
      this.items.push(d.data.items[0]);
      this.items.push(d.data.items[0]);
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
