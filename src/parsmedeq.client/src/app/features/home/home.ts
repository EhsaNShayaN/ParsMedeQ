import {Component} from '@angular/core';
import {BaseComponent} from '../../base-component';
import {JsonService} from '../../core/json.service';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class Home extends BaseComponent {
  public items: any[] = [];

  constructor(private jsonService: JsonService) {
    super();
    this.jsonService.getHomeCarouselSlides().subscribe(res => {
      this.items = res;
    });
  }
}
