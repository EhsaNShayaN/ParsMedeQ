import {Component, OnInit, ElementRef, AfterViewInit} from '@angular/core';
import {ClipService} from '../clip.service';
import {Clip} from '../models/clip.model';
import {MatDialog} from '@angular/material/dialog';
import {ClipDialogComponent} from '../clip-dialog/clip-dialog.component';

@Component({
  selector: 'app-clip-list',
  templateUrl: './clip-list.component.html',
  styleUrls: ['./clip-list.component.scss'],
  standalone: false
})
export class ClipListComponent implements OnInit, AfterViewInit {
  clips: Clip[] = [];

  constructor(
    private clipService: ClipService,
    private dialog: MatDialog,
    private el: ElementRef
  ) {
  }

  ngOnInit(): void {
    this.clipService.list().subscribe(res => this.clips = res);
  }

  openDialog(clip: Clip, event?: MouseEvent) {
    if (event) event.stopPropagation();
    this.dialog.open(ClipDialogComponent, {
      data: {clip},
      width: 'min(980px, 95vw)',
      panelClass: 'custom-clip-dialog',
      hasBackdrop: true,
      backdropClass: 'dark-backdrop'
    });
  }

  openDetail(c: Clip) {
    // navigate to detail route — we'll assume route exists
    // using window.location for simplicity; better to inject Router if prefer
    window.location.href = `/clips/${c.id}`;
  }

  ngAfterViewInit() {
    const io = new IntersectionObserver((entries) => {
      entries.forEach(e => {
        if (e.isIntersecting) {
          e.target.classList.add('visible');
          io.unobserve(e.target);
        }
      });
    }, {threshold: 0.1});

    this.el.nativeElement.querySelectorAll('.clip-card').forEach((n: HTMLElement) => io.observe(n));
  }

  getDate(publishDate: string) {
    return (new Date(publishDate)).toLocaleDateString('fa-IR');
  }
}
