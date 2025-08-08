import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ProductsRoutingModule} from './products-routing-module';
import {Products} from './products';
import {ProductList} from './product-list/product-list';
import {ProductDetail} from './product-detail/product-detail';
import {SharedModule} from '../../shared/shared-module';

@NgModule({
  declarations: [
    Products,
    ProductList,
    ProductDetail
  ],
  imports: [
    CommonModule,
    SharedModule,
    ProductsRoutingModule,
  ]
})
export class ProductsModule {
}
