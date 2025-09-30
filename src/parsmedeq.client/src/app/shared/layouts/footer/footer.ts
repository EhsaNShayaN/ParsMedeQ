import {Component} from '@angular/core';
import {AppSettings, Settings} from '../../../app.settings';

@Component({
  selector: 'app-footer',
  standalone: false,
  templateUrl: './footer.html',
  styleUrl: './footer.scss'
})
export class Footer {
  public settings: Settings;
  currentYear: number = new Date().getFullYear();

  constructor(private appSettings: AppSettings) {
    this.settings = this.appSettings.settings;
  }
}
