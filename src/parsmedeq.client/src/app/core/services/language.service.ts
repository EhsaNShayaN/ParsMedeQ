import {Injectable} from '@angular/core';
import {AppSettings} from '../../app.settings';

@Injectable({
  providedIn: 'root'
})

export class LanguageService {
  translateService: any;

  constructor(/*private translateService: TranslateService,*/
              private appSettings: AppSettings) {
  }

  addLanguages() {
    this.translateService.addLangs(['fa', 'en']);
  }

  getLanguages() {
    return this.translateService.getLangs();
  }

  getLang() {
    return this.translateService.getFallbackLang() ?? 'fa';
  }

  setLang(lang: string) {
    this.translateService.setFallbackLang(lang);
    this.translateService.use(lang);
    this.appSettings.settings.rtl = lang === 'fa';
  }
}
