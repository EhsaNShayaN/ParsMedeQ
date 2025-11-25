import {Component, OnInit} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {SectionService} from './section.service';
import {EditMainImageDialog} from './dialogs/edit-main-image.dialog';
import {EditServicesDialog} from './dialogs/edit-services.dialog';
import {EditAdvantagesDialog} from './dialogs/edit-advantages.dialog';
import {EditTextDialog} from './dialogs/edit-text.dialog';
import {EditBottomImageDialog} from './dialogs/edit-bottom-image.dialog';
import {BaseComponent} from '../../../base-component';

enum SectionType {
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
  // for complex sections:
  items?: any[]; // services or advantages items
  imageUrl?: string;
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

  constructor(
    private dialog: MatDialog,
    private service: SectionService) {
    super();
    this.languages = this.translateService.getLangs();
  }

  ngOnInit() {
    this.load();
  }

  load() {
    this.service.getAll().subscribe(res => {
      this.sections = res.data.sort((a, b) => a.order - b.order);
    });
  }

  edit(section: Section, lang: string) {
    switch (section.id) {
      case SectionType.mainImage:
        this.dialog.open(EditMainImageDialog, {width: '600px', data: section})
          .afterClosed().subscribe(res => {
          if (res) this.service.update(section.id, res).subscribe(() => this.load());
        });
        break;
      case SectionType.centers:
        this.router.navigate(['/admin/centers']); // صفحه مدیریت سانترها
        break;
      case SectionType.services:
        this.dialog.open(EditServicesDialog, {width: '800px', data: section})
          .afterClosed().subscribe(res => {
          if (res) this.service.update(section.id, res).subscribe(() => this.load());
        });
        break;
      case SectionType.advantages:
        this.dialog.open(EditAdvantagesDialog, {width: '800px', data: section})
          .afterClosed().subscribe(res => {
          if (res) this.service.update(section.id, res).subscribe(() => this.load());
        });
        break;
      case SectionType.about:
      case SectionType.contact:
        this.dialog.open(EditTextDialog, {width: '600px', data: section})
          .afterClosed().subscribe(res => {
          if (res) this.service.update(section.id, res).subscribe(() => this.load());
        });
        break;
      case SectionType.bottomImage:
        this.dialog.open(EditBottomImageDialog, {width: '600px', data: section})
          .afterClosed().subscribe(res => {
          if (res) this.service.update(section.id, res).subscribe(() => this.load());
        });
        break;
    }
  }

  toggle(section: Section) {
    this.service.toggle(section.id, section.hidden).subscribe(() => this.load());
  }
}
