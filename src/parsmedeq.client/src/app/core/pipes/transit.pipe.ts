import {Pipe, PipeTransform} from '@angular/core';

@Pipe({
  name: 'transit',
  standalone: false,
})
export class TransitPipe implements PipeTransform {
  transform(value: any): string {
    return value;
  }
}
