import {Pipe, PipeTransform} from '@angular/core';
import {AppSettings} from '../../app.settings';

@Pipe({
  name: 'langPack',
  standalone: false
})
export class LangPackPipe implements PipeTransform {

  constructor(private appSettings: AppSettings) {
  }

  transform(value: string): string {
    return this.appSettings.getUrlLang() + value;
  }
}
