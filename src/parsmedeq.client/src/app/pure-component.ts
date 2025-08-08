import {Directive, Injector} from '@angular/core';
import {UntypedFormGroup} from '@angular/forms';

@Directive()
export class PureComponent {
  constructor(injector: Injector) {
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
