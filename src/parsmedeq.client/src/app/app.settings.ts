import {Injectable} from '@angular/core';

export class Settings {
  constructor(public name: string,
              public theme: string,
              public toolbar: number,
              public stickyMenuToolbar: boolean,
              public rtl: boolean,
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
              public email: string,
              public tel: string,
              public tel1: string,
              public tel2: string,
              public postalCode: string,
              public mobile: string,
              public address: string,
              public selectLang: boolean
  ) {
  }
}

@Injectable()
export class AppSettings {
  public settings = new Settings(
    'ParsMedeQ',  // theme name
    'green-custom',      // blue, blue-dark, green, red, green-custom, pink, purple, grey, orange-dark
    1,           // 1 or 2 or 3
    true,        // true = sticky, false = not sticky
    true,       // true = rtl, false = ltr
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
    'info@parsmedeq.com',
    '02188222146',
    '02188222147',
    '02192003696',
    '1439813211',
    '0912-xxxxxxx',
    'امیرآباد – خیابان کارگر شمالی – خیابان فرشی مقدم (16) –<br>مرکز رشد پارک علم و فناوری – ساختمان دیتا سنتر- شرکت پیشرو فناوران درمان پارس',
    true
  );
}
