import {AfterViewInit, Component, OnInit} from '@angular/core';
import {SwiperConfigInterface} from 'ngx-swiper-wrapper';
import {PureComponent} from '../../../pure-component';
import {CenterModel, JsonService} from '../../../core/json.service';
import {animate, query, stagger, style, transition, trigger} from '@angular/animations';

@Component({
  selector: 'app-our-center',
  templateUrl: './our-center.component.html',
  styleUrl: './our-center.component.scss',
  standalone: false,
  animations: [trigger('centersAnimation', [
    transition('* => *', [
      query(':enter', [
        style({opacity: 0, transform: 'translateY(20px)'}),
        stagger(100, [animate('400ms ease-out', style({opacity: 1, transform: 'translateY(0)'}))])
      ], {optional: true})
    ])
  ])]
})
export class OurCenterComponent extends PureComponent implements OnInit, AfterViewInit {
  public items: CenterModel[] = [];
  public config: SwiperConfigInterface = {};

  constructor(private jsonService: JsonService) {
    super();
  }

  ngOnInit() {
    this.jsonService.getCenters().subscribe((d: CenterModel[]) => {
      this.items = d;
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

  openDialog(item: any) {
    this.dialogService.openCustomDialog(item.title, item.description, item.image);
  }
}
