import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Articles } from './articles';

const routes: Routes = [{ path: '', component: Articles }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ArticlesRoutingModule { }
