// src/app/products/product-list/product-list.component.ts
import {Component, OnInit} from '@angular/core';
import {Product} from './product.model';
import {ProductService} from './product.service';
import {Router} from '@angular/router';
import {trigger, transition, style, animate, query, stagger} from '@angular/animations';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'],
  animations: [
    trigger('listStagger', [
      transition('* => *', [
        query(':enter', [
          style({opacity: 0, transform: 'translateY(12px) scale(0.98)'}),
          stagger(80, [
            animate('360ms ease-out', style({opacity: 1, transform: 'translateY(0) scale(1)'}))
          ])
        ], {optional: true})
      ])
    ])
  ],
  standalone: false
})
export class ProductListComponent implements OnInit {
  protected readonly Math = Math;
  products: Product[] = [];
  loading = true;

  constructor(private productService: ProductService, private router: Router) {
  }

  ngOnInit() {
    this.productService.list().subscribe(res => {
      this.products = res;
      this.loading = false;
    });
  }

  goToDetail(p: Product) {
    this.router.navigate(['/products', p.id]);
  }

  shortPrice(p: Product): string {
    const price = p.price;
    return this.formatPrice(price);
  }

  formatPrice(n: number) {
    return n.toLocaleString('fa-IR') + ' تومان';
  }
}
