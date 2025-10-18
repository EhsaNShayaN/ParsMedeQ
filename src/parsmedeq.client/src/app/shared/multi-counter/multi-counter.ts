import {Component, ElementRef, HostListener, ViewChild} from '@angular/core';

@Component({
  selector: 'app-multi-counter',
  templateUrl: './multi-counter.html',
  styleUrls: ['./multi-counter.scss'],
  standalone: false
})
export class MultiCounterComponent {
  @ViewChild('counterSection') counterSection!: ElementRef;
  started = false;
  duration = 3000; // زمان انیمیشن به میلی‌ثانیه

  counters = [
    {label: 'سانترهای درمانی', target: 4, current: 0},
    {label: 'سال تجربه', target: 4, current: 0},
    {label: 'جوایز', target: 21, current: 0},
  ];

  @HostListener('window:scroll', [])
  onScroll() {
    if (this.started) return;

    const rect = this.counterSection.nativeElement.getBoundingClientRect();
    const isVisible = rect.top < window.innerHeight && rect.bottom >= 0;

    if (isVisible) {
      this.started = true;
      this.startCounters();
    }
  }

  startCounters() {
    const startTime = performance.now();

    const animate = (currentTime: number) => {
      const progress = Math.min((currentTime - startTime) / this.duration, 1);

      this.counters.forEach(c => {
        c.current = Math.floor(c.target * progress);
      });

      if (progress < 1) {
        requestAnimationFrame(animate);
      } else {
        this.counters.forEach(c => c.current = c.target);
      }
    };

    requestAnimationFrame(animate);
  }
}
