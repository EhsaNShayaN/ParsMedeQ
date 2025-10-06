import {Component, Input, HostListener, OnDestroy, OnInit} from '@angular/core';

@Component({
  selector: 'app-image-slider',
  templateUrl: './image-slider.html',
  styleUrl: './image-slider.scss',
  standalone: false,
})
export class ImageSliderComponent implements OnInit, OnDestroy {
  @Input() slides: Array<{
    section?: string;
    title?: string;
    description?: string;
    image: string;
    back?: string;
    ctaPrimaryText?: string;
    ctaPrimaryUrl?: string;
    ctaSecondaryText?: string;
    ctaSecondaryUrl?: string;
  }> = [];

  @Input() autoplay: boolean = false;
  @Input() autoplayIntervalMs: number = 5000;
  @Input() pauseOnHover: boolean = true;
  @Input() loop: boolean = true;
  @Input() enableSwipe: boolean = true;

  public currentIndex: number = 0;

  private autoplayTimerId: any = null;
  private isHovering: boolean = false;

  // swipe/drag state
  private pointerActive: boolean = false;
  private startX: number = 0;
  private deltaX: number = 0;
  private swipeThreshold: number = 150;

  ngOnInit(): void {
    this.startAutoplayIfNeeded();
  }

  ngOnDestroy(): void {
    this.clearAutoplay();
  }

  public goTo(index: number) {
    if (!this.slides?.length) {
      return;
    }
    const len = this.slides.length;
    if (!this.loop) {
      const clamped = Math.max(0, Math.min(index, len - 1));
      this.currentIndex = clamped;
      return;
    }
    this.currentIndex = ((index % len) + len) % len;
  }

  public next() {
    this.goTo(this.currentIndex + 1);
  }

  public prev() {
    this.goTo(this.currentIndex - 1);
  }

  public onMouseEnter(): void {
    if (this.pauseOnHover) {
      this.isHovering = true;
      this.clearAutoplay();
    }
  }

  public onMouseLeave(): void {
    if (this.pauseOnHover) {
      this.isHovering = false;
      this.startAutoplayIfNeeded();
    }
  }

  private startAutoplayIfNeeded(): void {
    if (!this.autoplay || this.slides.length <= 1) {
      return;
    }
    this.clearAutoplay();
    this.autoplayTimerId = setInterval(() => {
      if (!this.isHovering) {
        this.next();
      }
    }, Math.max(2000, this.autoplayIntervalMs || 0));
  }

  private clearAutoplay(): void {
    if (this.autoplayTimerId) {
      clearInterval(this.autoplayTimerId);
      this.autoplayTimerId = null;
    }
  }

  // Pointer/Touch handlers for swipe
  public onPointerDown(event: PointerEvent | TouchEvent | MouseEvent): void {
    if (!this.enableSwipe) {
      return;
    }
    const clientX = this.getClientX(event);
    this.pointerActive = true;
    this.startX = clientX;
    this.deltaX = 0;
  }

  public onPointerMove(event: PointerEvent | TouchEvent | MouseEvent): void {
    if (!this.enableSwipe || !this.pointerActive) {
      return;
    }
    const clientX = this.getClientX(event);
    this.deltaX = clientX - this.startX;
  }

  public onPointerUp(): void {
    if (!this.enableSwipe || !this.pointerActive) {
      return;
    }
    const moved = this.deltaX;
    this.pointerActive = false;
    this.startX = 0;
    this.deltaX = 0;
    if (Math.abs(moved) > this.swipeThreshold) {
      if (moved < 0) {
        this.next();
      } else {
        this.prev();
      }
    }
  }

  private getClientX(event: PointerEvent | TouchEvent | MouseEvent): number {
    // TouchEvent
    // @ts-ignore - TS in DOM libs varies per project
    if (event && (event.touches?.length || event.changedTouches?.length)) {
      // @ts-ignore
      const t = (event.touches && event.touches[0]) || (event.changedTouches && event.changedTouches[0]);
      return t?.clientX || 0;
    }
    // Pointer/Mouse
    // @ts-ignore
    return (event as any)?.clientX ?? 0;
  }

  // Keyboard navigation for accessibility
  @HostListener('keydown.arrowRight', ['$event'])
  onArrowRight(event: Event): void {
    (event as KeyboardEvent)?.preventDefault?.();
    this.next();
  }

  @HostListener('keydown.arrowLeft', ['$event'])
  onArrowLeft(event: Event): void {
    (event as KeyboardEvent)?.preventDefault?.();
    this.prev();
  }
}


