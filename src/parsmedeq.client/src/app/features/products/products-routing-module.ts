import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {Products} from './products';
import {ProductList} from './product-list/product-list';
import {ProductDetail} from './product-detail/product-detail';

const routes: Routes = [
  {path: '', component: Products},
  {path: 'list', component: ProductList},
  {path: ':id', component: ProductDetail}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductsRoutingModule {
}
