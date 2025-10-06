import {Component, OnInit, Input, OnChanges, OnDestroy, ViewChild} from '@angular/core';
import {Settings, AppSettings} from '../../app.settings';
import {SwiperDirective} from '../../theme/components/swiper/swiper.directive';
import {SwiperConfigInterface} from '../../theme/components/swiper/swiper.module';

@Component({
  selector: 'app-header-carousel',
  templateUrl: './header-carousel.component.html',
  styleUrls: ['./header-carousel.component.scss'],
  standalone: false
})
export class HeaderCarouselComponent implements OnInit, OnChanges, OnDestroy {
  @Input('slides') slides: Array<any> = [];
  @Input('contentOffsetToTop') contentOffsetToTop: boolean = false;
  @Input('fullscreen') fullscreen = false;
  public currentSlide: any;
  public currentIndex = 0;
  public settings: Settings;
  @ViewChild(SwiperDirective) directiveRef?: SwiperDirective;
  public config: SwiperConfigInterface = {};

  constructor(public appSettings: AppSettings) {
    this.settings = this.appSettings.settings;
  }

  ngOnInit() {
    if (this.contentOffsetToTop) {
      setTimeout(() => {
        this.settings.contentOffsetToTop = this.contentOffsetToTop;
      });
    }
    this.initCarousel();
  }

  public initCarousel() {
    this.config = {
      slidesPerView: 1,
      spaceBetween: 0,
      keyboard: false,
      navigation: true,
      pagination: false,
      grabCursor: true,
      loop: true,
      preloadImages: false,
      lazy: true,
      autoplay: {
        delay: 10000,
        disableOnInteraction: false
      },
      speed: 500,
      effect: 'slide',
    };
  }

  ngOnChanges() {
    if (this.slides.length > 0) {
      this.currentSlide = this.slides[0];
    }
  }

  ngOnDestroy() {
    setTimeout(() => {
      this.settings.contentOffsetToTop = false;
    });
  }

  onIndexChange($event: any) {
    this.currentIndex = $event;
    this.currentSlide = this.slides[this.currentIndex];
  }

  togglePlay() {
    if (this.config.autoplay) {
      this.config.autoplay = false;
    } else {
      this.config.autoplay = {
        delay: 5000,
        disableOnInteraction: false
      };
    }
  }

  goToIndex(i: number) {
    // this.onIndexChange(i);
    this.directiveRef?.setIndex(i);
  }
}
