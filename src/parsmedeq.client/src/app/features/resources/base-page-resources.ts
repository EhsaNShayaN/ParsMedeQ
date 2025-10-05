import {Directive} from '@angular/core';
import {BaseComponent} from '../../base-component';

@Directive()
export class BasePageResourceComponent extends BaseComponent {
  isLoading: boolean = true;

  constructor() {
    super();
  }
}
