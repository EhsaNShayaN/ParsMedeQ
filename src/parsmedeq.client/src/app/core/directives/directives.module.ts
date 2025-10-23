import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {OnlyNumberDirective} from './only-number.directive';
import {OnlyFloatNumberDirective} from './only-float-number.directive';
import {InputRestrictionDirective} from './app-input-restriction.directive';
import {NoSpaceDirective} from './no-space.directive';
import {DefaultImageDirective} from './default-image';
import {LoadingDirective} from '../../shared/loading/loading.directive';

@NgModule({
  declarations: [
    OnlyNumberDirective,
    OnlyFloatNumberDirective,
    InputRestrictionDirective,
    NoSpaceDirective,
    LoadingDirective,
    DefaultImageDirective,
  ],
  exports: [
    OnlyNumberDirective,
    OnlyFloatNumberDirective,
    InputRestrictionDirective,
    NoSpaceDirective,
    DefaultImageDirective,
    LoadingDirective,
  ],
  imports: [
    CommonModule,
  ]
})
export class DirectivesModule {
}
