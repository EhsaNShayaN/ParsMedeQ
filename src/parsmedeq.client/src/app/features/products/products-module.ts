import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ProductsRoutingModule} from './products-routing-module';
import {Products} from './products';
import {ProductComponent} from './product/product.component';
import {SharedModule} from '../../shared/shared-module';

@NgModule({
  declarations: [
    Products,
    ProductComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ProductsRoutingModule,
  ]
})
export class ProductsModule {
}
