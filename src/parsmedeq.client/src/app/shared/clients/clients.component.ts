import {AfterViewInit, Component} from '@angular/core';
import {SwiperConfigInterface, SwiperPaginationInterface} from 'ngx-swiper-wrapper';
import {BaseComponent} from "../../base-component";
import {JsonService} from "../../core/json.service";

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrl: './clients.component.scss',
  standalone: false
})
export class ClientsComponent extends BaseComponent implements AfterViewInit {
  public items: any[] = [];
  public config: SwiperConfigInterface = {};
  private pagination: SwiperPaginationInterface = {
    el: '.swiper-pagination',
    clickable: true
  };

  constructor(private jsonService: JsonService) {
    super();
    this.jsonService.getClients().subscribe(res => {
      this.items = res;
    });
  }

  ngAfterViewInit() {
    this.config = {
      observer: true,
      slidesPerView: 5,
      spaceBetween: 0,
      keyboard: false,
      navigation: true,
      pagination: this.pagination,
      grabCursor: true,
      loop: true,
      preloadImages: false,
      lazy: true,
      autoplay: {
        delay: 2000,
        disableOnInteraction: false
      },
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
          slidesPerView: 5
        }
      }
    };
  }
}
