import {Component, OnInit} from '@angular/core';
import {PureComponent} from '../../../pure-component';
import {getPathLang} from '../../../core/shared/util';
import {AppSettings} from '../../../app.settings';

@Component({
  selector: 'app-lang',
  templateUrl: './lang.html',
  styleUrl: './lang.scss',
  standalone: false
})
export class Lang extends PureComponent implements OnInit {
  public languages: readonly string[] = [];
  public langName = '';

  constructor(private appSettings: AppSettings) {
    super();
    this.languages = this.translateService.getLangs();
  }

  ngOnInit() {
    this.langName = this.getLangName(this.translateService.getDefaultLang());
  }

  public changeLang(lang: string) {
    const currentLang = this.translateService.getDefaultLang();
    if (lang === currentLang) {
      return;
    }
    localStorage.setItem('lang', lang);
    this.translateService.setDefaultLang(lang);
    this.langName = this.getLangName(lang);
    this.appSettings.settings.rtl = lang === 'fa';

    const currentUrlLang = getPathLang(this.languages);
    let path = window.location.pathname.replace(/^\/+/, '');
    if (currentUrlLang) {
      path = path.substring(2);
    }
    path = path && path.startsWith('/') ? path.substring(1) : path;
    window.location.href = `${window.location.origin}${lang === 'fa' ? '' : '/' + lang}/${path}`;
  }

  public getLangName(lang: string | null) {
    if (lang === 'fa') {
      return 'فارسی';
    } else if (lang === 'en') {
      return 'English';
    } else {
      return 'فارسی';
    }
  }
}
