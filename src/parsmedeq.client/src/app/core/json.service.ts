import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {AppSettings} from '../../../../app/app.settings';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JsonService {
  public url = '/assets/data/';

  constructor(private http: HttpClient,
              private appSettings: AppSettings) {
    this.url += this.appSettings.settings.lang + '/';
  }

  public getFirstResult() {
    return this.http.get<any[]>(this.url + 'first.json');
  }

  public getHomeCarouselSlides() {
    return this.http.get<any[]>(this.url + 'slides.json');
  }

  public getUsefulLinks() {
    return this.http.get<any[]>(this.url + 'useful-links.json');
  }

  public getAbout(name: string) {
    return this.http.get<any[]>(this.url + 'pages/about/' + name + '.json');
  }

  public getMarketerData(name: string): Observable<any> {
    return this.http.get<any[]>(this.url + 'pages/marketer/' + name + '.json');
  }
}
