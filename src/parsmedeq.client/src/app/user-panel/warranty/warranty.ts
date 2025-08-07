import {Component} from '@angular/core';

@Component({
  selector: 'app-warranty',
  standalone: false,
  templateUrl: './warranty.html',
  styleUrl: './warranty.scss'
})
export class Warranty {
  products = [
    {name: 'محصول A', daysLeft: 120},
    {name: 'محصول B', daysLeft: 35}
  ];
}
