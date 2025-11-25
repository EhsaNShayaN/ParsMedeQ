import {Component, OnInit} from '@angular/core';
import {SectionService} from './section.service';
import {BaseComponent} from '../../../base-component';

export enum SectionType {
  mainImage = 1,
  centers = 2,
  services = 3,
  advantages = 4,
  about = 5,
  contact = 6,
  bottomImage = 7
}

export interface Section {
  id: number;
  type: SectionType;
  title?: string;
  description?: string;
  hidden: boolean;
  order: number;
  items?: any[];
  image?: string;
}

@Component({
  selector: 'app-homepage-sections',
  templateUrl: './homepage-sections.component.html',
  styleUrls: ['./homepage-sections.component.scss'],
  standalone: false
})
export class HomepageSectionsComponent extends BaseComponent implements OnInit {
  languages: string[] = [];
  colors: string[] = ['warn', 'primary', 'success', 'secondary', 'info', 'danger'];
  displayedColumns = ['order', 'title', 'status', 'actions'];
  sections: Section[] = [];

  constructor(private service: SectionService) {
    super();
    this.languages = this.translateService.getLangs();
  }

  ngOnInit() {
    this.load();
  }

  load() {
    this.service.getAll().subscribe(res => {
      this.sections = res.data.filter(s => s.id !== SectionType.contact).sort((a, b) => a.order - b.order);
    });
  }

  toggle(section: Section) {
    this.service.toggle(section.id, section.hidden).subscribe(() => this.load());
  }
}
