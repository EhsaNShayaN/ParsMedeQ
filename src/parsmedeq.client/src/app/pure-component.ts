import {Directive, Injector} from '@angular/core';
import {UntypedFormGroup} from '@angular/forms';
import {RestApiService} from './core/rest-api.service';

@Directive()
export class PureComponent {
  restApiService: RestApiService;
  constructor(injector: Injector) {
    this.restApiService = injector.get(RestApiService);
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
