import {Injectable} from '@angular/core';

export class Settings {
  constructor(public name: string,
              public theme: string,
              public toolbar: number,
              public stickyMenuToolbar: boolean,
              public header: string,
              public rtl: boolean,
              public searchPanelVariant: number,
              public mainToolbarFixed: boolean,
              public contentOffsetToTop: boolean,
              public loadMore: {
                start: boolean,
                step: number,
                load: boolean,
                page: number,
                complete: boolean,
                result: number
              },
              public domain: string,
              public email: string,
              public mobile: string,
              public phones: string,
              public fax: string,
              public selectLang: boolean,
              public lang: string
  ) {
  }
}

@Injectable()
export class AppSettings {
  public settings = new Settings(
    'ParsMedeQ',  // theme name
    'red-custom',      // blue, blue-dark, green, red, red-custom, pink, purple, grey, orange-dark
    1,           // 1 or 2 or 3
    true,        // true = sticky, false = not sticky
    'carousel',     // default, image, carousel, map, video
    true,       // true = rtl, false = ltr
    2,           //  1, 2  or 3

    false,
    false,
    {
      start: false,
      step: 1,
      load: false,
      page: 1,
      complete: false,
      result: 0
    },
    'parsmedeq.com',
    'info@parsmedeq.com',
    '021-xxx',
    '021 xxx,021 xxxxxxxx,021 yyyyyyyy,021 zzzzzzzz',
    '021wwwwwwww',
    true,
    'fa'
  );

  public getUrlLang() {
    return this.settings.lang === 'fa' ? '' : '/' + this.settings.lang;
  }
}
