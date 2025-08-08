import {Component} from '@angular/core';

export interface OrderModel {
  id: number;
  productName: string;
  date: Date;
}

@Component({
  selector: 'app-orders',
  standalone: false,
  templateUrl: './orders.html',
  styleUrl: './orders.scss'
})
export class Orders {
  orders: OrderModel[] = [
    {id: 101, productName: 'دستگاه تست A', date: new Date()},
    {id: 102, productName: 'دستگاه تست B', date: new Date()}
  ];
}
