import {AfterViewInit, Component, ElementRef} from '@angular/core';
import {BasePageResource} from '../../base-page-resource';
import {Tables} from '../../../../core/constants/server.constants';
import {DomSanitizer} from '@angular/platform-browser';
import {ClipDialogComponent} from '../clip-dialog/clip-dialog.component';
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'app-clip',
  templateUrl: './clip.component.html',
  styleUrls: ['./clip.component.scss'],
  standalone: false,
})
export class ClipComponent extends BasePageResource implements AfterViewInit {
  constructor(private dialog: MatDialog,
              private el: ElementRef) {
    super(Tables.Clip);
  }

  override ngAfterViewInit() {
    super.ngAfterViewInit();
    setTimeout(() => {
      this.el.nativeElement.querySelectorAll('.fade-in').forEach((s: HTMLElement) => s.classList.add('visible'));
    }, 80);
  }

  openPlayer() {
    if (!this.item) return;
    this.dialog.open(ClipDialogComponent, {
      data: {clip: this.item},
      width: 'min(980px, 95vw)',
      panelClass: 'custom-clip-dialog',
      hasBackdrop: true,
      backdropClass: 'dark-backdrop'
    });
  }
}
