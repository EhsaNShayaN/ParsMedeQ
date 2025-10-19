// src/app/services/product.service.ts
import {Injectable} from '@angular/core';
import {Product} from './product.model';
import {of, Observable} from 'rxjs';

@Injectable({providedIn: 'root'})
export class ProductService {
  private products: Product[] = [
    {
      id: 1,
      title: 'مانیتور علائم حیاتی PM-200',
      description: 'مانیتوری دقیق و قابل اعتماد با نمایش چند پارامتر حیاتی.',
      price: 12500000,
      discountPercent: 10,
      category: 'مانیتورها',
      coverImage: 'assets/images/p1.jpg',
      gallery: ['assets/images/p1-1.jpg', 'assets/images/p1-2.jpg', 'assets/images/p1-3.jpg'],
      specifications: [
        {key: 'وزن', value: '3.2 کیلوگرم'},
        {key: 'صفحه نمایش', value: '12 اینچ رنگی'},
        {key: 'برند', value: 'ParsMEDEQ'}
      ],
      inStock: true,
      rating: 4.6
    },
    {
      id: 2,
      title: 'دستگاه نوار قلب ECG-X1',
      description: 'قابل حمل با کیفیت ثبت بالا و خروجی قابل اتصال به سیستم HIS.',
      price: 8500000,
      discountPercent: 0,
      category: 'ECG',
      coverImage: 'assets/images/p2.jpg',
      gallery: ['assets/images/p2-1.jpg', 'assets/images/p2-2.jpg'],
      specifications: [
        {key: 'کانال', value: '12 کانال'},
        {key: 'وزن', value: '2.1 کیلوگرم'}
      ],
      inStock: true,
      rating: 4.3
    },
    // ... موارد بیشتر
  ];

  list(): Observable<Product[]> {
    return of(this.products);
  }

  getById(id: number): Observable<Product | undefined> {
    const p = this.products.find(x => x.id === +id);
    return of(p);
  }
}
