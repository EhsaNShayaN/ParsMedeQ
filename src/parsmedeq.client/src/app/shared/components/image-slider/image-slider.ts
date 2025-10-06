import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-image-slider',
  templateUrl: './image-slider.html',
  styleUrl: './image-slider.scss',
  standalone: false,
})
export class ImageSliderComponent {
  @Input() slides: Array<{
    section?: string;
    title?: string;
    description?: string;
    image: string;
    back?: string;
  }> = [];

  public currentIndex: number = 0;

  public goTo(index: number) {
    if (!this.slides?.length) {
      return;
    }
    const len = this.slides.length;
    this.currentIndex = ((index % len) + len) % len;
  }

  public next() {
    this.goTo(this.currentIndex + 1);
  }

  public prev() {
    this.goTo(this.currentIndex - 1);
  }
}


