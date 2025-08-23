import {Component, OnInit} from '@angular/core';
import {PureComponent} from '../../../pure-component';
import {LanguageService} from '../../../core/services/language.service';
import {getPathLang} from '../../../core/shared/util';

@Component({
  selector: 'app-lang',
  templateUrl: './lang.html',
  styleUrl: './lang.scss',
  standalone: false
})
export class Lang extends PureComponent implements OnInit {
  public languages: readonly string[] = [];
  public langName = '';

  constructor(private languageService: LanguageService) {
    super();
    this.languages = languageService.getLanguages();
  }

  ngOnInit() {
    this.langName = this.getLangName(this.languageService.getLang());
  }

  public changeLang(lang: string) {
    this.languageService.setLang(lang);
    this.langName = this.getLangName(lang);

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
