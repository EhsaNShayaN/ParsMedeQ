import {Directive, inject} from '@angular/core';
import {UntypedFormGroup} from '@angular/forms';
import {RestApiService} from './core/rest-api.service';
import {TranslateService} from '@ngx-translate/core';
import {Router} from '@angular/router';

@Directive()
export class PureComponent {
  restApiService = inject(RestApiService);
  translateService = inject(TranslateService);
  router = inject(Router);

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

  navigateToLink(url: string, lang: string) {
    if (lang !== 'fa') {
      url = lang + '/' + url;
    }
    this.router.navigate([url]).then(() => {
    });
  }

  getTranslateValue(key: string, param: string | null = null) {
    let value = null;
    this.translateService.get(key, {param}).subscribe((res: string) => {
      value = res;
    });
    return value ?? '';
  }
}
