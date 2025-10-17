import {AfterViewInit, Component} from '@angular/core';
import {PureComponent} from '../../../pure-component';
import {animate, query, stagger, style, transition, trigger} from '@angular/animations';
import {SwiperConfigInterface, SwiperPaginationInterface} from 'ngx-swiper-wrapper';
import {JsonService} from '../../../core/json.service';

export interface CenterModel {
  title: string;
  image: string;
  city: string;
  description: string;
}

@Component({
  selector: 'app-our-centers',
  templateUrl: './our-centers.component.html',
  styleUrl: './our-centers.component.scss',
  standalone: false,
  animations: [
    trigger('centersAnimation', [
      transition('* => *', [
        query(':enter', [
          style({opacity: 0, transform: 'translateY(20px)'}),
          stagger(100, [animate('400ms ease-out', style({opacity: 1, transform: 'translateY(0)'}))])
        ], {optional: true})
      ])
    ])
  ]
})
export class OurCentersComponent extends PureComponent implements AfterViewInit {
  public centers: CenterModel[] = [];
  public config: SwiperConfigInterface = {};
  private pagination: SwiperPaginationInterface = {
    el: '.swiper-pagination',
    clickable: true
  };

  constructor(private jsonService: JsonService) {
    super();
    this.jsonService.getCenters().subscribe(res => {
      this.centers = res.slice(0, 4);
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

  openDialog(item: CenterModel) {
    this.dialogService.openCustomDialog(item.title, item.city + '<br/>' + item.description, item.image);
  }
}
