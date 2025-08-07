import {Component, signal} from '@angular/core';
import {RestApiService} from './core/rest-api.service';
import {WeatherForecast} from './core/models/WeatherForecast';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  standalone: false,
})
export class App {
  protected readonly title = signal('parsmedeq.client');
  forecasts: WeatherForecast[] = [];

  constructor(private restApiService: RestApiService) {
    restApiService.getWeatherForecast().subscribe((r: WeatherForecast[]) => {
      this.forecasts = r;
    });
  }
}
