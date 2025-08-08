import {Injectable} from '@angular/core';

export class Settings {
  constructor(public name: string,
              public theme: string,
              public toolbar: number,
              public stickyMenuToolbar: boolean,
              public header: string,
              public rtl: boolean,
              public searchPanelVariant: number,
              public searchOnBtnClick: boolean,
              public currency: string,
              public mainToolbarFixed: boolean,
              public contentOffsetToTop: boolean,
              public headerBgImage: boolean,
              public headerBgVideo: boolean,
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
              public selectCurrency: boolean,
              public lang: string
  ) {
  }
}

@Injectable()
export class AppSettings {
  public settings = new Settings(
    'AlborzChem',  // theme name
    'red-custom',      // blue, blue-dark, green, red, red-custom, pink, purple, grey, orange-dark
    1,           // 1 or 2 or 3
    true,        // true = sticky, false = not sticky
    'carousel',     // default, image, carousel, map, video
    true,       // true = rtl, false = ltr
    2,           //  1, 2  or 3
    false,       //  true = search on button click
    'USD',       // USD, EUR

    // NOTE:  don't change additional options values, they used for theme performance
    false,
    false,
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
    'alborzchem.com',
    'info@alborzchem.com',
    '021-58128',
    '021 58128,021 22026611,021 22026622,021 22050250',
    '02122022840',
    true,
    false,
    'fa'
  );
}
