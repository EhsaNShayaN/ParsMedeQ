import {Pipe, PipeTransform} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';

@Pipe({
  name: 'langPack',
  standalone: false
})
export class LangPackPipe implements PipeTransform {

  constructor(private translateService: TranslateService) {
  }

  transform(value: string): string {
    const currentLang = this.translateService.getDefaultLang();
    if (currentLang === 'fa') return value;
    return '/' + currentLang + value;
  }
}
