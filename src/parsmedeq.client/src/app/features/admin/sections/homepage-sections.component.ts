import {Component, OnInit} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {CdkDragDrop, moveItemInArray} from '@angular/cdk/drag-drop';
import {Router} from '@angular/router';
import {SectionService} from './section.service';
import {EditMainImageDialog} from './dialogs/edit-main-image.dialog';
import {EditServicesDialog} from './dialogs/edit-services.dialog';
import {EditAdvantagesDialog} from './dialogs/edit-advantages.dialog';
import {EditTextDialog} from './dialogs/edit-text.dialog';
import {EditBottomImageDialog} from './dialogs/edit-bottom-image.dialog';

export type SectionType =
  | 'mainImage'
  | 'centers'
  | 'services'
  | 'advantages'
  | 'about'
  | 'contact'
  | 'bottomImage';

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
export class HomepageSectionsComponent implements OnInit {
  displayedColumns = ['order', 'title', 'status', 'actions'];
  sections: Section[] = [];

  constructor(
    private dialog: MatDialog,
    private service: SectionService,
    private router: Router
  ) {
  }

  ngOnInit() {
    this.load();
  }

  load() {
    this.service.getAll().subscribe(res => {
      this.sections = res.data.sort((a, b) => a.order - b.order);
    });
  }

  edit(section: Section) {
    switch (section.type) {
      case 'mainImage':
        this.dialog.open(EditMainImageDialog, {width: '600px', data: section})
          .afterClosed().subscribe(res => {
          if (res) this.service.update(section.id, res).subscribe(() => this.load());
        });
        break;
      case 'centers':
        this.router.navigate(['/admin/centers']); // صفحه مدیریت سانترها
        break;
      case 'services':
        this.dialog.open(EditServicesDialog, {width: '800px', data: section})
          .afterClosed().subscribe(res => {
          if (res) this.service.update(section.id, res).subscribe(() => this.load());
        });
        break;
      case 'advantages':
        this.dialog.open(EditAdvantagesDialog, {width: '800px', data: section})
          .afterClosed().subscribe(res => {
          if (res) this.service.update(section.id, res).subscribe(() => this.load());
        });
        break;
      case 'about':
      case 'contact':
        this.dialog.open(EditTextDialog, {width: '600px', data: section})
          .afterClosed().subscribe(res => {
          if (res) this.service.update(section.id, res).subscribe(() => this.load());
        });
        break;
      case 'bottomImage':
        this.dialog.open(EditBottomImageDialog, {width: '600px', data: section})
          .afterClosed().subscribe(res => {
          if (res) this.service.update(section.id, res).subscribe(() => this.load());
        });
        break;
    }
  }

  toggle(section: Section) {
    this.service.update(section.id, {hidden: !section.hidden}).subscribe(() => this.load());
  }

  drop(event: CdkDragDrop<Section[]>) {
    moveItemInArray(this.sections, event.previousIndex, event.currentIndex);
    const ordered = this.sections.map((s, index) => ({id: s.id, order: index + 1}));
    this.service.updateOrder(ordered).subscribe(() => this.load());
  }
}
