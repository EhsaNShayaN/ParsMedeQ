import {Inject, Injectable} from '@angular/core';
import {getPathLang} from '../shared/util';
import {NavigationEnd, Router} from '@angular/router';
import {filter} from 'rxjs';
import {DOCUMENT} from '@angular/common';
import {TranslateService} from '@ngx-translate/core';
import {AppSettings} from '../../app.settings';

@Injectable({
  providedIn: 'root',
})
export class UrlInitService {
  constructor(@Inject(DOCUMENT) private document: Document,
              private router: Router,
              private translateService: TranslateService,
              private appSettings: AppSettings) {
    console.log('UrlInitService');
  }

  init(): Promise<void> {
    this.translateService.addLangs(['fa', 'en']);
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        this.setCanonical();
      });
    return new Promise((resolve) => {
      if (typeof window !== 'undefined') {
        const languages = this.translateService.getLangs();
        const lang = getPathLang(languages) ?? 'fa';
        localStorage.setItem('lang', lang);
        this.translateService.setDefaultLang(lang);
        this.appSettings.settings.rtl = lang === 'fa';
        console.log('current lang', lang);
      }
      resolve();
    });
  }

  x(lang: string, path: string): string {
    const p = path && path.startsWith('/') ? path.substring(1) : path;
    const l = lang === 'fa' ? '' : `/${lang}`;
    return `${window.location.origin}${l}${p ? `/${p}` : ''}`;
  }

  setCanonical() {
    const languages: readonly any[] = this.translateService.getLangs();
    const currentUrlLang = getPathLang(languages);
    let path = window.location.pathname.replace(/^\/+/, '');
    this.remove('canonical');
    if (currentUrlLang) {
      path = path.substring(currentUrlLang.length);
      if (currentUrlLang === 'fa') {
        this.updateMeta('canonical', this.x('fa', path));
      }
    }
    this.remove('alternate');
    for (const lang of languages) {
      if (currentUrlLang && currentUrlLang === lang) continue;
      this.updateAlternate('alternate', lang, this.x(lang, path));
    }
    this.updateAlternate('alternate', 'x-default', this.x('fa', path));
  }

  private updateMeta(property: string, content: string) {
    let link: HTMLLinkElement | null = this.document.querySelector(`link[rel=${property}]`);
    if (!link && content) {
      link = this.document.createElement('link');
      link.setAttribute('rel', property);
      this.document.head.appendChild(link);
    }
    if (link) {
      if (content) {
        link.setAttribute('href', content);
      } else {
        this.document.head.removeChild(link);
      }
    }
  }

  private updateAlternate(rel: string, hrefLang: string, content: string) {
    let link: HTMLLinkElement | null = this.document.querySelector(`link[rel=${rel}][hreflang=${hrefLang}]`);
    if (!link) {
      link = this.document.createElement('link');
      link.setAttribute('rel', rel);
      link.setAttribute('hrefLang', hrefLang);
      this.document.head.appendChild(link);
    }
    link.setAttribute('href', content);
  }

  remove(rel: string) {
    let links: NodeListOf<Element> | null = this.document.querySelectorAll(`link[rel=${rel}]`);
    if (!links) {
      return;
    }
    for (const link of links) {
      this.document.head.removeChild(link);
    }
  }
}
