import {Component, OnInit, Input, OnDestroy} from '@angular/core';
import {DomSanitizer, SafeStyle} from '@angular/platform-browser';
import {Settings, AppSettings} from '../../app.settings';

@Component({
  selector: 'app-header-image',
  templateUrl: './header-image.component.html',
  styleUrl: './header-image.component.scss',
  standalone: false
})
export class HeaderImageComponent implements OnInit, OnDestroy {
  @Input('backgroundImage') backgroundImage?: string;
  @Input('bgImageAnimate') bgImageAnimate: boolean = false;
  @Input('contentOffsetToTop') contentOffsetToTop: boolean = false;
  @Input('hideMask') hideMask: boolean = true;
  @Input('contentMinHeight') contentMinHeight: number = 0;
  @Input('title') title?: string;
  @Input('desc') desc?: string;
  @Input('isHomePage') isHomePage: boolean = false;
  @Input('fullscreen') fullscreen: boolean = false;
  public bgImage?: any;
  public settings: Settings;

  constructor(public appSettings: AppSettings, private sanitizer: DomSanitizer) {
    this.settings = this.appSettings.settings;
  }

  ngOnInit() {
    if (this.contentOffsetToTop) {
      setTimeout(() => {
        this.settings.contentOffsetToTop = this.contentOffsetToTop;
      });
    }
    if (this.backgroundImage) {
      this.bgImage = this.sanitizer.bypassSecurityTrustStyle('url(' + this.backgroundImage + ')');
    }
  }

  ngOnDestroy() {
    setTimeout(() => {
      this.settings.contentOffsetToTop = false;
    });
  }
}
