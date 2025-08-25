import {Component, OnInit} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import {LanguageService} from '../../core/services/language.service';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnInit {
  currentLang: string = 'fa';

  constructor(private translateService: TranslateService,
              private languageService: LanguageService
  ) {
    // Ensure Persian is set as default
    if (!this.translateService.currentLang) {
      this.translateService.use('fa');
    }
  }

  ngOnInit() {
    console.log('Current language from service:', this.languageService.getLang());
    console.log('Current language from translate service:', this.translateService.currentLang);

    this.currentLang = this.languageService.getLang();

    // Subscribe to language changes
    this.translateService.onLangChange.subscribe((event) => {
      console.log('Language changed to:', event.lang);
      this.currentLang = event.lang;
    });
  }

  changeLanguage(lang: string) {
    console.log('Changing language to:', lang);
    this.languageService.setLang(lang);
  }

  resetToPersian() {
    this.languageService.resetToPersian();
  }
}
