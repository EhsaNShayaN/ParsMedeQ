import {Directive, inject} from '@angular/core';
import {UntypedFormGroup} from '@angular/forms';
import {RestApiService} from './core/rest-api.service';

@Directive()
export class PureComponent {
  restApiService = inject(RestApiService);

  constructor() {
  }

  findInvalidControls(myForm: UntypedFormGroup) {
    const invalid = [];
    const controls = myForm.controls;
    for (const name in controls) {
      if (controls[name].invalid) {
        invalid.push(name);
      }
    }
    return invalid;
  }
}
