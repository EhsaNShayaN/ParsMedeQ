import {Component, AfterViewInit, ElementRef, HostListener} from '@angular/core';

@Component({
  selector: 'app-home2',
  templateUrl: './home2.html',
  styleUrls: ['./home2.scss'],
  standalone: false
})
export class Home2 implements AfterViewInit {

  constructor(private el: ElementRef) {
  }

  ngAfterViewInit() {
    // Scroll animation observer
    const observer = new IntersectionObserver(
      (entries) => {
        entries.forEach(entry => {
          if (entry.isIntersecting) {
            entry.target.classList.add('visible');
            observer.unobserve(entry.target);
          }
        });
      },
      {threshold: 0.2}
    );

    this.el.nativeElement
      .querySelectorAll('.scroll-section')
      .forEach((section: HTMLElement) => observer.observe(section));

    // Initial parallax update
    this.updateParallax();
  }

  @HostListener('window:scroll', [])
  onScroll() {
    window.requestAnimationFrame(() => this.updateParallax());
  }

  updateParallax() {
    const scrolled = window.scrollY;
    const hero = this.el.nativeElement.querySelector('.hero');
    const cta = this.el.nativeElement.querySelector('.cta');

    if (hero) hero.style.backgroundPositionY = `${scrolled * 0.4}px`;
    if (cta) cta.style.backgroundPositionY = `${scrolled * 0.2 - 100}px`;
  }
}
