import { Component } from '@angular/core';

@Component({
  selector: 'app-products',
  standalone: false,
  templateUrl: './products.html',
  styleUrl: './products.scss'
})
export class Products {
  products = [
    { id: 1, title: 'محصول A', shortDescription: 'توضیح مختصر محصول A' },
    { id: 2, title: 'محصول B', shortDescription: 'توضیح مختصر محصول B' }
  ];
}
