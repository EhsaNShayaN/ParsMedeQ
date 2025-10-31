import {Component, HostListener, OnDestroy, OnInit} from '@angular/core';
import {BaseComponent} from '../../../base-component';
import {ActivatedRoute} from '@angular/router';
import {Product, ProductImage} from '../../../core/models/ProductResponse';
import {ProductCategoriesResponse, ProductCategory} from '../../../core/models/ProductCategoryResponse';
import {BaseResult} from '../../../core/models/BaseResult';
import {Tables} from '../../../core/constants/server.constants';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss',
  standalone: false
})
export class ProductComponent extends BaseComponent implements OnInit, OnDestroy {
  protected readonly Tables = Tables;
  private sub: any;
  product?: Product;
  productCategories: ProductCategory[] = [];
  parents: ProductCategory[] = [];
  fixTabs = false;
  selectedImage?: string;

  constructor(private activatedRoute: ActivatedRoute) {
    super();
  }

  ngOnInit() {
    this.restApiService.getProductCategories().subscribe((res: ProductCategoriesResponse) => {
      this.productCategories = res.data;
      this.sub = this.activatedRoute.params.subscribe(params => {
        this.restApiService.getProduct({id: params['id']}).subscribe((p: BaseResult<Product>) => {
          this.product = p.data;
          this.selectedImage = p?.data?.images[0]?.path;
          console.log(this.selectedImage);
          this.getParents(this.product?.productCategoryId);
          this.setTitle(this.product.title);
          this.setMetaDescription(this.product.description);
        });
      });
    });
  }

  getParents(categoryId: number | undefined) {
    if (categoryId) {
      const parent = this.productCategories.find(s => s.id === categoryId);
      if (parent) {
        this.parents.push(parent);
        if (parent.parentId) {
          this.getParents(parent.parentId);
        }
      }
    }
  }

  @HostListener('window:scroll') onWindowScroll() {
    const scrollTop = Math.max(window.pageYOffset, document.documentElement.scrollTop, document.body.scrollTop);
    const productItem = document.getElementById('product-item');
    this.fixTabs = (scrollTop + 150) > (productItem?.offsetTop ?? 0);
  }

  selectImage(productImage: ProductImage) {
    this.selectedImage = productImage.path;
  }

  finalPrice(p: Product): number {
    if (!p) return 0;
    if (p.discount && p.discount > 0) {
      return Math.round((p.price ?? 0) * (1 - p.discount / 100));
    }
    return p.price ?? 0;
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}

