import {Inject, Injectable} from '@angular/core';
import {LanguageService} from './language.service';
import {getPathLang} from '../shared/util';
import {NavigationEnd, Router} from '@angular/router';
import {filter} from 'rxjs';
import {DOCUMENT} from '@angular/common';
import {Language} from '@ngx-translate/core';

@Injectable({
  providedIn: 'root',
})
export class UrlInitService {
  constructor(@Inject(DOCUMENT) private document: Document,
              private router: Router,
              private languageService: LanguageService) {
  }

  init(): Promise<void> {
    this.languageService.addLanguages();
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        this.setCanonical();
      });
    return new Promise((resolve) => {
      if (typeof window !== 'undefined') {
        const languages = this.languageService.getLanguages();
        const lang = getPathLang(languages) ?? 'fa';
        this.languageService.setLang(lang);
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
    const languages: readonly Language[] = this.languageService.getLanguages();
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
