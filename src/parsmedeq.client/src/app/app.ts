import {Component, signal} from '@angular/core';
import {RestApiService} from '../../lib/core/rest-api.service';
import {WeatherForecast} from '../../lib/models/WeatherForecast';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
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
