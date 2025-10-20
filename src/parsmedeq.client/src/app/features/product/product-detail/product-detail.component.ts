import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {Product} from '../models/product.model';
import {ProductService} from '../product.service';
import {Tables} from '../../../core/constants/server.constants';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss'],
  standalone: false
})
export class ProductDetailComponent implements OnInit {
  product?: Product;
  coverImage?: string;
  selectedImage?: string;
  loading = true;
  quantity = 1;

  constructor(private route: ActivatedRoute,
              private productService: ProductService) {
  }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.productService.getById(+id).subscribe(p => {
      this.product = p;
      this.coverImage = p?.coverImage;
      this.selectedImage = p?.gallery[0];
      this.loading = false;

      // Debug: Check if cover image is loaded
      console.log('Product loaded:', p);
      console.log('Cover image:', this.coverImage);
    });
  }

  selectImage(url: string) {
    this.selectedImage = url;
  }

  finalPrice(p: Product): number {
    if (!p) return 0;
    if (p.discountPercent && p.discountPercent > 0) {
      return Math.round(p.price * (1 - p.discountPercent / 100));
    }
    return p.price;
  }

  formatPrice(n: number) {
    return n.toLocaleString('fa-IR') + ' تومان';
  }

  protected readonly Tables = Tables;
}
