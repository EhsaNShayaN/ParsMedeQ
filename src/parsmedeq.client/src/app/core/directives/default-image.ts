import {Directive, HostListener, Input} from '@angular/core';

@Directive({
  selector: 'img[defaultImage]',
  standalone: false
})
export class DefaultImageDirective {
  @Input() defaultImage: string | undefined; // your default image path

  @HostListener('error', ['$event'])
  onError(event: Event) {
    const element = event.target as HTMLImageElement;
    element.src = this.getDefaultImage();
  }

  @HostListener('load', ['$event'])
  onLoad(event: Event) {
    const element = event.target as HTMLImageElement;
    if (!element.src || element.src.trim() === '') {
      element.src = this.getDefaultImage();
    }
  }

  getDefaultImage() {
    if (this.defaultImage) {
      return this.defaultImage;
    }
    return '/assets/no-image.jpg';
  }
}
