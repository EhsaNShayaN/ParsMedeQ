import {Component} from '@angular/core';

@Component({
  selector: 'app-orders',
  standalone: false,
  templateUrl: './orders.html',
  styleUrl: './orders.scss'
})
export class Orders {
  orders = [
    {id: 101, productName: 'دستگاه تست A', date: new Date()},
    {id: 102, productName: 'دستگاه تست B', date: new Date()}
  ];
}
