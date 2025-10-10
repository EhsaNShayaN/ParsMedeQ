import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {Products} from './products';
import {ProductComponent} from './product/product.component';

const routes: Routes = [
  {path: '', component: Products},
  {path: ':id', component: ProductComponent},
  {path: ':id/:title', component: ProductComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductsRoutingModule {
}
