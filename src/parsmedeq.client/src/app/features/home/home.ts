import {AfterViewInit, Component, ElementRef, HostListener, OnInit} from '@angular/core';
import {animate, style, transition, trigger} from '@angular/animations';
import {SectionService} from '../admin/sections/section.service';
import {Section} from '../admin/sections/homepage-sections.component';
import {MainSections, SectionType} from '../../core/constants/server.constants';

@Component({
  selector: 'app-home',
  templateUrl: './home.html',
  styleUrls: ['./home.scss'],
  standalone: false,
  animations: [trigger('fadeInSimple', [
    transition(':enter', [
      style({opacity: 0, transform: 'translateY(12px)'}),
      animate('420ms ease-out', style({opacity: 1, transform: 'translateY(0)'}))
    ])
  ])]
})
export class Home implements OnInit, AfterViewInit {
  mainSections = MainSections;
  sections: Section[] = [];

  mainImageIsHide: boolean = false;
  centersIsHide: boolean = false;
  servicesIsHide: boolean = false;
  advantagesIsHide: boolean = false;
  aboutIsHide: boolean = false;
  contactIsHide: boolean = false;
  bottomImageIsHide: boolean = false;

  mainImageSections: Section[] = [];
  centersSections: Section[] = [];
  servicesSections: Section[] = [];
  advantagesSections: Section[] = [];
  aboutSections: Section[] = [];
  contactSections: Section[] = [];
  bottomImageSections: Section[] = [];

  constructor(private el: ElementRef,
              private sectionService: SectionService) {
    this.sectionService.getAllItems().subscribe(res => {
      this.sections = res.data;
      this.mainSections.forEach(m => {
        const section = this.sections.find(s => s.sectionId === m.sectionId);
        m.hidden = section?.hidden ?? false;
      });
      this.mainImageIsHide = this.mainSections.find(s => s.type === SectionType.mainImage).hidden;
      this.centersIsHide = this.mainSections.find(s => s.type === SectionType.centers).hidden;
      this.servicesIsHide = this.mainSections.find(s => s.type === SectionType.services).hidden;
      this.advantagesIsHide = this.mainSections.find(s => s.type === SectionType.advantages).hidden;
      this.aboutIsHide = this.mainSections.find(s => s.type === SectionType.about).hidden;
      this.contactIsHide = this.mainSections.find(s => s.type === SectionType.contact).hidden;
      this.bottomImageIsHide = this.mainSections.find(s => s.type === SectionType.bottomImage).hidden;

      this.mainImageSections = this.sections.filter(s => s.sectionId === SectionType.mainImage);
      this.centersSections = this.sections.filter(s => s.sectionId === SectionType.centers);
      this.servicesSections = this.sections.filter(s => s.sectionId === SectionType.services);
      this.advantagesSections = this.sections.filter(s => s.sectionId === SectionType.advantages);
      this.aboutSections = this.sections.filter(s => s.sectionId === SectionType.about);
      this.contactSections = this.sections.filter(s => s.sectionId === SectionType.contact);
      this.bottomImageSections = this.sections.filter(s => s.sectionId === SectionType.bottomImage);
    });
  }

  ngOnInit(): void {
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
    if (cta) cta.style.backgroundPositionY = `${scrolled * 0.2 - 1000}px`;
  }
}
