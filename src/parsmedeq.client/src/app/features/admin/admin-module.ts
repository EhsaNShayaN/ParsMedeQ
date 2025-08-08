import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AdminRoutingModule} from './admin-routing-module';
import {Admin} from './admin';
import {Dashboard} from './dashboard/dashboard';
import {Users} from './users/users';
import {Products} from './products/products';
import {Articles} from './articles/articles';
import {SharedModule} from '../../shared/shared-module';


@NgModule({
  declarations: [
    Admin,
    Dashboard,
    Users,
    Products,
    Articles
  ],
  imports: [
    CommonModule,
    SharedModule,
    AdminRoutingModule,
  ]
})
export class AdminModule {
}
