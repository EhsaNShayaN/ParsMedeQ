import {Component, OnInit, signal} from '@angular/core';
import {RestApiService} from './core/rest-api.service';
import {WeatherForecast} from './core/models/WeatherForecast';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  standalone: false,
})
export class App implements OnInit {
  protected readonly title = signal('parsmedeq.client');
  forecasts: WeatherForecast[] = [];

  constructor(private restApiService: RestApiService) {
  }

  ngOnInit() {
    this.restApiService.getWeatherForecast().subscribe((r: WeatherForecast[]) => {
      this.forecasts = r;
    });
    this.restApiService.addResource().subscribe((r: any) => {
      console.log(r);
      console.log(r.data);
    });
  }
}
