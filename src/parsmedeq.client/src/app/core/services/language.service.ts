import {Injectable} from '@angular/core';
import {AppSettings} from '../../app.settings';
import {TranslateService} from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})

export class LanguageService {
  constructor(private translateService: TranslateService,
              private appSettings: AppSettings) {
    // Set default language to Persian
    this.translateService.setDefaultLang('fa');
    this.translateService.addLangs(['fa', 'en']);
    
    // Get language from localStorage or use default
    const savedLang = localStorage.getItem('language') || 'fa';
    
    // Ensure the language is set properly
    this.translateService.use(savedLang);
    this.appSettings.settings.rtl = savedLang === 'fa';
    this.appSettings.settings.lang = savedLang;
  }

  addLanguages() {
    this.translateService.addLangs(['fa', 'en']);
  }

  getLanguages() {
    return this.translateService.getLangs();
  }

  getLang() {
    return this.translateService.currentLang || 'fa';
  }

  setLang(lang: string) {
    console.log('Setting language to:', lang);
    this.translateService.use(lang);
    localStorage.setItem('language', lang);
    this.appSettings.settings.rtl = lang === 'fa';
    this.appSettings.settings.lang = lang;
  }

  resetToPersian() {
    console.log('Resetting to Persian');
    localStorage.removeItem('language');
    this.setLang('fa');
  }
}
