import {Component, OnInit, ElementRef, AfterViewInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {ClipService} from '../clip.service';
import {Clip} from '../models/clip.model';
import {MatDialog} from '@angular/material/dialog';
import {ClipDialogComponent} from '../clip-dialog/clip-dialog.component';
import {BaseComponent} from '../../../base-component';

@Component({
  selector: 'app-clip-detail',
  templateUrl: './clip-detail.component.html',
  styleUrls: ['./clip-detail.component.scss'],
  standalone: false
})
export class ClipDetailComponent extends BaseComponent implements OnInit, AfterViewInit {
  clip?: Clip;
  loading = true;

  constructor(private route: ActivatedRoute,
              private service: ClipService,
              private dialog: MatDialog,
              private el: ElementRef) {
    super();
  }

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.service.getById(id).subscribe(c => {
      this.clip = c;
      this.loading = false;
    });
  }

  openPlayer() {
    if (!this.clip) return;
    this.dialog.open(ClipDialogComponent, {
      data: {clip: this.clip},
      width: 'min(980px, 95vw)',
      panelClass: 'custom-clip-dialog',
      hasBackdrop: true,
      backdropClass: 'dark-backdrop'
    });
  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.el.nativeElement.querySelectorAll('.fade-in').forEach((s: HTMLElement) => s.classList.add('visible'));
    }, 80);
  }

  goToClip(c: Clip) {
    this.router.navigate(['/clips', c.id]);
  }

  getDate(publishDate: string) {
    return (new Date(publishDate)).toLocaleDateString('fa-IR');
  }
}
