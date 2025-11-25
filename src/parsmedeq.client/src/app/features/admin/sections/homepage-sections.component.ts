import {Component, OnInit} from '@angular/core';
import {SectionService} from './section.service';
import {BaseComponent} from '../../../base-component';
import {MainSections, SectionType} from '../../../core/constants/server.constants';

export interface Section {
  id: number;
  sectionId: number;
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
  protected readonly SectionType = SectionType;
  languages: string[] = [];
  colors: string[] = ['warn', 'primary', 'success', 'secondary', 'info', 'danger'];
  displayedColumns = ['order', 'title', 'status', 'actions'];
  mainSections= MainSections;

  constructor(private service: SectionService) {
    super();
    this.languages = this.translateService.getLangs();
  }

  ngOnInit() {
    this.load();
  }

  load() {
    this.service.getAll().subscribe(res => {
      const sections = res.data.sort((a, b) => a.order - b.order);
      this.mainSections.forEach(s => {
        s.hidden = sections.find(s => s.id === s.id)?.hidden;
      });
    });
  }

  toggle(section: Section) {
    this.service.toggle(section.sectionId, section.hidden).subscribe(() => this.load());
  }
}
