import {Directive, HostListener} from '@angular/core';

@Directive({
  selector: 'input[onlyNumber]',
  standalone: false
})
export class OnlyNumberDirective {

  constructor() {
  }

  @HostListener('keypress', ['$event']) onInputChange(e: any) {

    /*if (e.target.value.length === 0 && e.which === 48) {
      console.log('ok');
      console.log(e.target.value.length);
      console.log(e.which);
      return false;
    }*/

    const verified = String.fromCharCode(e.which).match(/[^0-9]/g);
    if (verified) {
      e.preventDefault();
      return false;
    }
    return true;
    // var regex = new RegExp("[^0-9]");
    // var key = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    // if (regex.test(key)) {
    //     event.preventDefault();
    //     return false;
    // }

  }
}
